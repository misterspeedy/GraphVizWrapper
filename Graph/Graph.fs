#if INTERACTIVE
#else
namespace GraphVizWrapper
#endif

open System
open Microsoft.FSharp.Reflection
open System.Collections.Generic
open System.Drawing

[<AutoOpen>]
module __ =
   let getUnionCaseName (x:'a) = 
      match FSharpValue.GetUnionFields(x, typeof<'a>) with
      | case, _ -> case.Name  
   let isNumber s =
      match Double.TryParse(s) with
      | true, _ -> true
      | _ -> false
   let isBool s =
      s = "true" || s = "false"
   let quote s =
      sprintf "\"%s\"" s
   let dictToAttrList (dict : Dictionary<string, string>) =
      let pairs = 
         if dict.Count > 0 then
            dict
            |> Seq.map (fun kvp -> 
               let valueStr =
                  if kvp.Value |> isNumber || kvp.Value |> isBool then
                     kvp.Value
                  else
                     kvp.Value |> quote
               sprintf "%s = %s" kvp.Key valueStr)
            |> Array.ofSeq
         else
            [||]
      if pairs.Length > 0 then
         sprintf "  [ %s ]" (String.Join("; ", pairs))
      else
         ""

   let colorToString (c : Color) =
      if c.IsNamedColor then
         c.Name
      else
         sprintf "#%02x%02x%02x%02x" c.A c.R c.G c.B

type Strictness =
| Strict
| NonStrict
   override this.ToString() =
      match this with
      | Strict -> "strict"
      | NonStrict -> ""

type GraphKind =
| Graph
| Digraph
   override this.ToString() =
      (getUnionCaseName this).ToLowerInvariant()

type Id =
| Id of string
   override this.ToString() =
      match this with
      | Id s -> s

type GraphNode(id : Id) =
   member __.Id = id
   override this.ToString() =
      this.Id.ToString()

// TODO Should it really be possible to add directed edges to graphs
// and undirected edges to digraphs?
type Directionality =
| Directed
| Undirected
   override this.ToString() =
      match this with
      | Directed -> "->"
      | Undirected -> "--"

type Statement =
| NodeStatement of GraphNode
| EdgeStatement of GraphNode * GraphNode * Directionality
   override this.ToString() =
      match this with
      | NodeStatement n -> 
         sprintf "\"%O\"" n.Id
      | EdgeStatement(n1, n2, d) ->
         sprintf "\"%O\" %O \"%O\"" n1 d n2

type Statements(statements : Statement list) =
   member __.Statements = statements
   member this.WithStatement(statement : Statement) =
      Statements(statement::this.Statements |> List.rev)
   override __.ToString() =
      if statements.IsEmpty then 
         ""
      else
         sprintf "%s\r\n" 
            (String.Join (";\r\n", 
               [| for statement in statements -> 
                     sprintf "  %O" statement |]))

type AttributeStatementType =
| Graph
| Node
| Edge
   override this.ToString() =
      (getUnionCaseName this).ToLowerInvariant()

type Attribute (key : Id, value : Id) =
   member __.Key = key
   member __.Value = value
   override this.ToString() =
      sprintf "\"%O\" = \"%O\"" this.Key this.Value

type Attributes(statementType : AttributeStatementType, attributes : Attribute list) =
   member __.StatementType = statementType
   member __.Attributes = attributes
   member this.WithAttribute(attribute : Attribute) =
      Attributes(this.StatementType, attribute::this.Attributes)
   override this.ToString() =
      if attributes.IsEmpty then 
         ""
      else
         sprintf "  %O [ %s ]\r\n"
            this.StatementType
            (String.Join (";", [| for attribute in attributes -> attribute.ToString() |]))

type GraphVizRect = 
   {
      llx : double
      lly : double
      urx : double
      ury : double
   }
      override this.ToString() =
         sprintf "%g,%g,%g,%g" this.llx this.lly this.urx this.ury

type WeightedColor =
   {
      WColor : Color
      Weight : float option
   }
      override this.ToString() =
         sprintf "%s%s" 
            (this.WColor |> colorToString) 
            (match this.Weight with | Some w -> sprintf ";%g" w | None -> "")

type WeightedColorList() =
   let mutable list : WeightedColor list = []
   let totalWeight items =
      items |> List.sumBy (fun wc -> match wc.Weight with | Some w -> w | _ -> 0.)
   member this.Add(weightedColor : WeightedColor) =
      let newList = weightedColor :: list
      if newList |> totalWeight <= 1.0 then
         list <- newList
         this
      else
         raise (ArgumentException("Color weights cannot add up to more than 1.0"))
   override __.ToString() =
      let items = 
         list
         |> List.rev
         |> List.map (fun wc -> wc.ToString())
         |> Array.ofList
      String.Join(":", items)

type GraphColor =
| SingleColor of Color
| ColorList of WeightedColorList
   override this.ToString() =
      match this with
      | SingleColor c -> c |> colorToString
      | ColorList cl -> cl.ToString()

type Dimension(n : int) =
   do
      if n < 2 || n > 10 then
         raise (ArgumentOutOfRangeException("Dimension value must be between 2 and 10 inclusive"))
   member __.N = n

type DirEdgeConstraints =
| False
| True
| Hier
   override this.ToString() =
      (getUnionCaseName this).ToLowerInvariant()

type Separation(h : double, w : double, additive : bool) =
   new (p : double, additive) =
      Separation(p, p, additive)
   member __.H = h
   member __.W = w
   member __.Additive = additive
   override __.ToString() =
      sprintf "%s%s"
         (if additive then "+" else "")
         (if h = w then sprintf "%g" h else sprintf "%g,%g" h w)

type Graph
   (
      id : Id, 
      strictness: Strictness, 
      kind : GraphKind, 
      statements : Statements
//      graphAttributes : Attributes,
//      nodeAttributes : Attributes,
//      edgeAttributes : Attributes
   ) =
   let defaultDamping = 0.99
   let defaultK = 0.3
   let defaultUrl = ""
   let defaultBackground = ""
   let defaultBb : GraphVizRect option = None
   // TODO colorlist
   let defaultBgColor : GraphColor option = None
   let defaultCenter = false
   let defaultCharSet = "UTF-8"
   let defaultClusterRank = "local"
   let defaultColor : GraphColor = GraphColor.SingleColor(Color.Black)
   let defaultColorScheme = ""
   let defaultComment = ""
   let defaultCompound = false
   let defaultConcentrate = false
   let defaultDefaultDistance : float option = None
   let defaultDim = Dimension(2)
   let defaultDimen = Dimension(2)
   let defaultDirEdgeConstraints = DirEdgeConstraints.False
   let defaultDpi = 96
   let defaultEpsilon : float option = None
   let defaultESep : Separation option = None
   new (id : Id, strictness: Strictness, kind : GraphKind) =
      Graph(id, strictness, kind, Statements([])) 
//         Attributes(AttributeStatementType.Graph, []),
//         Attributes(AttributeStatementType.Node, []),
//         Attributes(AttributeStatementType.Edge, []))
   member __.Id = id
   member __.Strictness = strictness
   member __.Kind = kind
   member __.Statements = statements
   member val Damping = defaultDamping with get, set
   member val K = defaultK with get, set
   member val Url = defaultUrl with get, set
   member val Background = defaultBackground with get, set
   member val Bb = defaultBb with get, set
   member val BgColor = defaultBgColor with get, set
   member val Center = defaultCenter with get, set
   member val Charset = defaultCharSet with get, set
   member val ClusterRank = defaultClusterRank with get, set
   member val Color = defaultColor with get, set
   member val ColorScheme = defaultColorScheme with get, set
   member val Comment = defaultComment with get, set
   member val Compound = defaultCompound with get, set
   member val Concentrate = defaultConcentrate with get, set
   member val DefaultDistance = defaultDefaultDistance with get, set
   member val Dim = defaultDim with get, set
   member val Dimen = defaultDimen with get, set
   member val DirEdgeConstraints = defaultDirEdgeConstraints with get, set
   member val Dpi = defaultDpi with get, set
   member val Epsilon = defaultEpsilon with get, set
   member val ESep = defaultESep with get, set
   member private this.GraphAttributes =
      let dict = Dictionary<string, string>()
      // TODO could consider putting an attribute on the relevant members
      // and iterating over them using reflection
      if this.Damping <> defaultDamping then
         dict.["Damping"] <- this.Damping.ToString()
      if this.K <> defaultK then
         dict.["K"] <- this.K.ToString()
      if this.Url <> defaultUrl then
         dict.["URL"] <- this.Url
      if this.Background <> defaultBackground then
         dict.["_background"] <- this.Background
      match this.Bb with
      | Some r -> dict.["bb"] <- r.ToString()
      | _ -> ()
      match this.BgColor with
      | Some c -> 
         dict.["bgcolor"] <- c.ToString()
      | _ -> ()
      if this.Center <> defaultCenter then
         dict.["center"] <- (defaultCenter |> not).ToString().ToLowerInvariant()
      if this.Charset <> defaultCharSet then
         dict.["charset"] <- this.Charset
      if this.ClusterRank <> defaultClusterRank then
         dict.["clusterrank"] <- this.ClusterRank
      if this.Color <> defaultColor then
         dict.["color"] <- this.Color.ToString()
      if this.ColorScheme <> defaultColorScheme then
         dict.["colorscheme"] <- this.ColorScheme
      if this.Comment <> defaultComment then
         dict.["comment"] <- this.Comment
      if this.Compound <> defaultCompound then
         dict.["compound"] <- (defaultCompound |> not).ToString().ToLowerInvariant()
      if this.Concentrate <> defaultConcentrate then
         dict.["concentrate"] <- (defaultConcentrate |> not).ToString().ToLowerInvariant()
      match this.DefaultDistance with
      | Some d -> 
         dict.["defaultdist"] <- sprintf "%g" d
      | None -> ()
      if this.Dim <> defaultDim then
         dict.["dim"] <- this.Dim.N.ToString()
      if this.Dimen <> defaultDimen then
         dict.["dimen"] <- this.Dimen.N.ToString()
      if this.DirEdgeConstraints <> defaultDirEdgeConstraints then
         dict.["diredgeconstraints"] <- this.DirEdgeConstraints.ToString()
      if this.Dpi <> defaultDpi then
         dict.["dpi"] <- this.Dpi.ToString()
      match this.Epsilon with
      | Some e -> dict.["epsilon"] <- sprintf "%g" e
      | None -> ()
      match this.ESep with
      | Some es -> dict.["esep"] <- es.ToString()
      | None -> ()

      dict |> dictToAttrList
//   member __.GraphAttributes = graphAttributes
//   member __.NodeAttributes = nodeAttributes
//   member __.EdgeAttributes = edgeAttributes
   member this.WithStatement(statement : Statement) =
      Graph(this.Id, this.Strictness, this.Kind, this.Statements.WithStatement statement)
      //, this.GraphAttributes, this.NodeAttributes, this.EdgeAttributes)
//   member this.WithGraphAttribute(attribute : Attribute) =
//      Graph(this.Id, this.Strictness, this.Kind, this.Statements, this.GraphAttributes.WithAttribute attribute, this.NodeAttributes, this.EdgeAttributes)
//   member this.WithNodeAttribute(attribute : Attribute) =
//      Graph(this.Id, this.Strictness, this.Kind, this.Statements, this.GraphAttributes, this.NodeAttributes.WithAttribute attribute, this.EdgeAttributes)
//   member this.WithEdgeAttribute(attribute : Attribute) =
//      Graph(this.Id, this.Strictness, this.Kind, this.Statements, this.GraphAttributes, this.NodeAttributes, this.EdgeAttributes.WithAttribute attribute)
   override this.ToString() =
      (
         sprintf "\
            %O %O \"%O\"\r\n\
            {\r\n\
               %O\
               %O\
            }\
         " 
            this.Strictness
            this.Kind
            this.Id
            this.GraphAttributes
//            this.NodeAttributes
//            this.EdgeAttributes
            this.Statements
      ).Trim()
