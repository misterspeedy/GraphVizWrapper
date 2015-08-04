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
   let isNumber (s : string) =
      // Commas are allowed by Double.TryParse, so sabotage that:
      match Double.TryParse(s.Replace(',', 'x')) with
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

type FontNames =
| Undefined
| Svg
| Ps
| Gd
   override this.ToString() =
      (getUnionCaseName this).ToLowerInvariant()

type FontSize(points : double) =
   do
      if points < 1.0 then
         raise (ArgumentOutOfRangeException("Font size must be at least 1.0"))
   member __.Points = points
   override __.ToString() =
      sprintf "%g" points

type ImagePath(elements : string[]) =
   let onWindows =
      // Ugh! Really?
      Environment.OSVersion.Platform.ToString().StartsWith("Win")
   member __.Elements = elements
   override this.ToString() =
      let sep = if onWindows then ";" else ":"
      String.Join(sep, this.Elements)

type LabelScheme =
| NoScheme
| PenaltyCenter
| PenaltyCenterOld
| TwoStep
   override this.ToString() =
      match this with
      | NoScheme -> "0"
      | PenaltyCenter -> "1"
      | PenaltyCenterOld -> "2"
      | TwoStep -> "3"

type LabelJust =
| Center
| Right
| Left
   override this.ToString() =
      match this with
      | Center -> "c"
      | Right -> "r"
      | Left -> "l"

type LabelLoc =
| Top
| Center
| Bottom
   override this.ToString() =
      match this with
      | Top -> "t"
      | Center -> "c"
      | Bottom -> "b"

type Layer(name : string, selected : bool) =
   member val Name = name with get
   member val Selected = selected with get

type Layers(layers : Layer[]) =
   member __.Layers = layers
   member __.ToString(sep) =
      let names = layers |> Array.map (fun layer -> layer.Name)
      String.Join(sep, names)
   member this.AllSelected =
      this.Layers 
      |> Array.exists (fun l -> l.Selected |> not) 
      |> not
   member this.LayerSelect(sep) =
      let layerIndices =
         this.Layers
         |> Array.mapi (fun i l -> i+1, l)
         |> Array.filter (fun (_, l) -> l.Selected)
         |> Array.map fst
         |> Array.map (fun n -> n.ToString())
      String.Join(sep, layerIndices)

type GraphPoint(x : float, y : float) =
   member __.X = x
   member __.Y = y
   override __.ToString() =
      sprintf "%g,%g" x y

type Margin(size : GraphPoint) =
   member __.Size = size
   new (singleSize : float) =
      Margin(GraphPoint(singleSize, singleSize))
   override __.ToString() =
      if size.X = size.Y then
         sprintf "%g" size.X 
      else
         size.ToString()

type Mode =
| Major
| KK
| Hier
| IpSep
| Spring
| MaxEnt
   override this.ToString() =
      match this with
      | KK -> "KK"
      | _ -> (getUnionCaseName this).ToLowerInvariant()

type Model =
| ShortPath
| Circuit
| Subset
| Mds
   override this.ToString() = 
      (getUnionCaseName this).ToLowerInvariant()

type NodeSep(inches : double) =
   do
      if inches < 0.02 then
         raise (ArgumentOutOfRangeException("Node separation must be at least 0.02 inches"))
   member __.Inches = inches
   override __.ToString() =
      sprintf "%g" inches

type Normalize =
| Bool of bool
| Degrees of float
   override this.ToString() =
      match this with
      | Bool b -> b.ToString().ToLowerInvariant()
      | Degrees d -> sprintf "%g" d

type Ordering =
| Out
| In
   override this.ToString() =
      (getUnionCaseName this).ToLowerInvariant()

type OutputOrder =
| BreadthFirst
| NodesFirst
| EdgesFirst
   override this.ToString() =
      (getUnionCaseName this).ToLowerInvariant()

type Overlap =
| True  
| False  
| Scale  
| Prism  
| PrismN of suffix:int  
| Voronoi  
| ScaleXY  
| Compress  
| Vpsc  
| Ortho  
| OrthoXY  
| OrthoYX  
| Ortho_YX  
| POrtho  
| POrthoXY  
| POrthoYX  
| POrtho_YX  
| IpSep  
   member this.ToString(prefix : int) =
      let s = 
         match this with
         | PrismN n -> sprintf "prism%i" n
         | _ -> (getUnionCaseName this).ToLowerInvariant()
      if prefix > 0 then
         sprintf "%i:%s" prefix s
      else
         s

type Pack =
| True of margin:int option
| False
   override this.ToString() =
      match this with
      | True(Some m) -> sprintf "%i" m
      | True(None) -> sprintf "true"
      | False -> sprintf "false"

type PackModeOrdering =
| Default
| ColumnMajor
| ColumnMajorN of int
   override this.ToString() =
      match this with
      | Default -> ""
      | ColumnMajor -> "c"
      | ColumnMajorN n -> sprintf "c%i" n

type PackModeAlignment =
| Default
| Top 
| Bottom
| Left
| Right
   override this.ToString() =
      match this with
      | Default -> ""
      | _ -> (getUnionCaseName this).ToLowerInvariant().[0].ToString()

type PackModeInsertionOrder =
| Default
| User
   override this.ToString() =
      match this with
      | Default -> ""
      | _ -> (getUnionCaseName this).ToLowerInvariant().[0].ToString()

type PackMode =
| Node
| Clust
| Graph
| Array of ordering:PackModeOrdering * alignment:PackModeAlignment * insertionOrder:PackModeInsertionOrder
   override this.ToString() =
      match this with
      | Array(ordering, alignment, insertionOrder) ->
         let flags = sprintf "%s%s%s" (ordering.ToString()) (alignment.ToString()) (insertionOrder.ToString())
         if flags.Length > 0 then sprintf "array_%s" flags else "array"
      | _ -> (getUnionCaseName this).ToLowerInvariant()

// TODO on this and margin and page, consider having the primary constructor take the two args
type Pad(size : GraphPoint) =
   member __.Size = size
   new (singleSize : float) =
      Pad(GraphPoint(singleSize, singleSize))
   override __.ToString() =
      if size.X = size.Y then
         sprintf "%g" size.X 
      else
         size.ToString()

type Page(size : GraphPoint) =
   member __.Size = size
   new (singleSize : float) =
      Page(GraphPoint(singleSize, singleSize))
   override __.ToString() =
      if size.X = size.Y then
         sprintf "%g" size.X 
      else
         size.ToString()

type PageDir =
| BottomLeft
| BottomRight
| TopLeft
| TopRight
| RightBottom
| RightTop
| LeftBottom
| LeftTop
   override this.ToString() =
      let chars = 
         this
         |> getUnionCaseName
         |> Seq.filter (fun s -> s.ToString().ToUpperInvariant() = s.ToString())
         |> Array.ofSeq
      String(chars)

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
   let defaultFontColor = Color.Black
   let defaultFontName = "Times-Roman"
   let defaultFontNames = FontNames.Undefined
   let defaultFontPath = ""
   let defaultFontSize = FontSize(14.0)
   let defaultForceLabels = true
   let defaultGradientAngle = 0
   let defaultIdAttribute = ""
   let defaultImagePath = ImagePath([||])
   let defaultInputScale = 72.0
   let defaultLabel = ""
   let defaultLabelScheme = LabelScheme.NoScheme
   let defaultLabelJust = LabelJust.Center
   let defaultLabelLoc : LabelLoc option = None
   let defaultRotation = 0
   let defaultLayerListSep = ","
   let defaultLayers = Layers([||])
   let defaultLayerSep = ":"
   let defaultLayout = ""
   let defaultLevels = Int32.MaxValue
   let defaultLevelsGap = 0.0
   let defaultLHeight = 0.0
   let defaultLp : GraphPoint option = None
   let defaultLWidth = 0.0
   let defaultMargin = Margin(0.0)
   let defaultMaxIter = 0
   let defaultMcLimit = 1.0
   let defaultMinDist = 1.0
   let defaultMode = Mode.Major
   let defaultModel = Model.ShortPath
   let defaultMosek = false
   let defaultNodeSep = NodeSep(0.25)
   let defaultNoJustify = false
   let defaultNormalize = Normalize.Bool(false)
   let defaultNoTranslate = false
   let defaultNsLimit = 0.0
   let defaultNsLimit1 = 0.0
   let defaultOrdering : Ordering option = None
   let defaultOrientation = 0.0
   let defaultOutputOrder = OutputOrder.BreadthFirst
   let defaultOverlap : Overlap option = None
   let defaultOverlapPrefix = 0
   let defaultOverlapScaling = -4.0
   let defaultOverlapShrink = true
   let defaultPack = Pack.False
   let defaultPackMode = PackMode.Node
   let defaultPad = Pad(0.0555)
   let defaultPage : Page option = None
   let defaultPageDir = PageDir.BottomLeft
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
   member val FontColor = defaultFontColor with get, set
   member val FontName = defaultFontName with get, set
   member val FontNames = defaultFontNames with get, set
   member val FontPath = defaultFontPath with get, set
   member val FontSize = defaultFontSize with get, set
   member val ForceLabels = defaultForceLabels with get, set
   member val GradientAngle = defaultGradientAngle with get, set
   // Href is a synonym for url:
   member this.Href 
      with get() = this.Url
      and set(value) = this.Url <- value
   /// Rendered as 'id' in the attribute string but called 'IdAttribute' here
   /// to avoid a naming clash with the Graph Id.
   member val IdAttribute = defaultIdAttribute with get, set
   member val ImagePath = defaultImagePath with get, set
   member val InputScale = defaultInputScale with get, set
   member val Label = defaultLabel with get, set
   member val LabelScheme = defaultLabelScheme with get, set
   member val LabelJust = defaultLabelJust with get, set
   member val LabelLoc = defaultLabelLoc with get, set
   member this.Landscape
      with get() = this.Rotation = 90
      and set(value) = 
         if value then this.Rotation <- 90
         else if this.Rotation = 90 then this.Rotation <- 0
   member val LayerListSep = defaultLayerListSep with get, set
   member val Layers = defaultLayers with get, set
   member val LayerSep = defaultLayerSep with get, set
   member val Layout = defaultLayout with get, set
   member val Levels = defaultLevels with get, set
   member val LevelsGap = defaultLevelsGap with get, set
   member val LHeight = defaultLHeight with get, set
   member val Lp = defaultLp with get, set
   member val LWidth = defaultLWidth with get, set
   member val Margin = defaultMargin with get, set
   member val MaxIter = defaultMaxIter with get, set
   member val McLimit = defaultMcLimit with get, set
   member val MinDist = defaultMinDist with get, set
   member val Mode = defaultMode with get, set
   member val Model = defaultModel with get, set
   member val Mosek = defaultMosek with get, set
   member val NodeSep = defaultNodeSep with get, set
   member val NoJustify = defaultNoJustify with get, set
   member val Normalize = defaultNormalize with get, set
   member val NoTranslate = defaultNoTranslate with get, set
   member val NsLimit = defaultNsLimit with get, set
   member val NsLimit1 = defaultNsLimit1 with get, set
   member val Ordering = defaultOrdering with get, set
   // TODO wrap over 360?
   member val Orientation = defaultOrientation with get, set 
   // TODO wrap over 360?
   member val Rotation = defaultRotation with get, set
   member val OutputOrder = defaultOutputOrder with get, set
   member val Overlap = defaultOverlap with get, set
   member val OverlapPrefix = defaultOverlapPrefix with get, set
   member val OverlapScaling = defaultOverlapScaling with get, set
   member val OverlapShrink = defaultOverlapShrink with get, set
   member val Pack = defaultPack with get, set
   member val PackMode = defaultPackMode with get, set
   member val Pad = defaultPad with get, set
   member val Page = defaultPage with get, set
   member val PageDir = defaultPageDir with get, set
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
         dict.["center"] <- this.Center.ToString().ToLowerInvariant()
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
         dict.["compound"] <- this.Compound.ToString().ToLowerInvariant()
      if this.Concentrate <> defaultConcentrate then
         dict.["concentrate"] <- this.Concentrate.ToString().ToLowerInvariant()
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
      if this.FontColor <> defaultFontColor then
         dict.["fontcolor"] <- this.FontColor |> colorToString
      if this.FontName <> defaultFontName then
         dict.["fontname"] <- this.FontName
      if this.FontNames <> defaultFontNames then
         dict.["fontnames"] <- this.FontNames.ToString()
      if this.FontPath <> defaultFontPath then
         dict.["fontpath"] <- this.FontPath
      if this.FontSize <> defaultFontSize then
         dict.["fontsize"] <- this.FontSize.ToString()
      if this.ForceLabels <> defaultForceLabels then
         dict.["forcelabels"] <- this.ForceLabels.ToString().ToLowerInvariant()
      if this.GradientAngle <> defaultGradientAngle then
         dict.["gradientangle"] <- this.GradientAngle.ToString()
      if this.IdAttribute <> defaultIdAttribute then
         dict.["id"] <- this.IdAttribute
      if this.ImagePath <> defaultImagePath then
         dict.["imagepath"] <- this.ImagePath.ToString()
      if this.InputScale <> defaultInputScale then
         dict.["inputscale"] <- sprintf "%g" this.InputScale
      if this.Label <> defaultLabel then
         dict.["label"] <- this.Label
      if this.LabelScheme <> defaultLabelScheme then
         dict.["labelscheme"] <- this.LabelScheme.ToString()
      if this.LabelJust <> defaultLabelJust then
         dict.["labeljust"] <- this.LabelJust.ToString()
      match this.LabelLoc with
      | Some loc -> dict.["labelloc"] <- loc.ToString()
      | None -> ()
      if this.Rotation <> defaultRotation then
         dict.["rotation"] <- this.Rotation.ToString()
      if this.LayerListSep <> defaultLayerListSep then
         dict.["layerlistsep"] <- this.LayerListSep
      if this.Layers <> defaultLayers then
         dict.["layers"] <- this.Layers.ToString(this.LayerSep)
      if this.LayerSep <> defaultLayerSep then
         dict.["layersep"] <- this.LayerSep
      if this.Layers.AllSelected |> not then
         dict.["layerselect"] <- this.Layers.LayerSelect(this.LayerListSep)
      if this.Layout <> defaultLayout then
         dict.["layout"] <- this.Layout
      if this.Levels <> defaultLevels then
         dict.["levels"] <- this.Levels.ToString()
      if this.LevelsGap <> defaultLevelsGap then
         dict.["levelsgap"] <- sprintf "%g" this.LevelsGap
      if this.LHeight <> defaultLHeight then
         dict.["lheight"] <- sprintf "%g" this.LHeight
      match this.Lp with
      | Some(lp) -> dict.["lp"] <- lp.ToString()
      | None -> ()
      if this.LWidth <> defaultLWidth then
         dict.["lwidth"] <- sprintf "%g" this.LWidth
      if this.Margin <> defaultMargin then
         dict.["margin"] <- this.Margin.ToString()
      if this.MaxIter <> defaultMaxIter then
         dict.["maxiter"] <- this.MaxIter.ToString()
      if this.McLimit <> defaultMcLimit then
         dict.["mclimit"] <- sprintf "%g" this.McLimit
      if this.MinDist <> defaultMinDist then
         dict.["mindist"] <- sprintf "%g" this.MinDist
      if this.Mode <> defaultMode then
         dict.["mode"] <- this.Mode.ToString()
      if this.Model <> defaultModel then
         dict.["model"] <- this.Model.ToString()
      if this.Mosek <> defaultMosek then
         dict.["mosek"] <- this.Mosek.ToString().ToLowerInvariant()
      if this.NodeSep <> defaultNodeSep then
         dict.["nodesep"] <- this.NodeSep.ToString()
      if this.NoJustify <> defaultNoJustify then
         dict.["nojustify"] <- this.NoJustify.ToString().ToLowerInvariant()
      if this.Normalize <> defaultNormalize then
         dict.["normalize"] <- this.Normalize.ToString()
      if this.NoTranslate <> defaultNoTranslate then
         dict.["notranslate"] <- this.NoTranslate.ToString().ToLowerInvariant()
      if this.NsLimit <> defaultNsLimit then
         dict.["nslimit"] <- sprintf "%g" this.NsLimit
      if this.NsLimit1 <> defaultNsLimit1 then
         dict.["nslimit1"] <- sprintf "%g" this.NsLimit1
      match this.Ordering with
      | Some o -> dict.["ordering"] <- o.ToString()
      | None -> ()
      if this.Orientation <> defaultOrientation then
         dict.["orientation"] <- sprintf "%g" this.Orientation
      if this.OutputOrder <> defaultOutputOrder then
         dict.["outputorder"] <- this.OutputOrder.ToString()
      match this.Overlap with
      | Some o -> dict.["overlap"] <- o.ToString(this.OverlapPrefix)
      | None -> ()
      if this.OverlapScaling <> defaultOverlapScaling then
         dict.["overlap_scaling"] <- sprintf "%g" this.OverlapScaling
      if this.OverlapShrink <> defaultOverlapShrink then
         dict.["overlap_shrink"] <- this.Center.ToString().ToLowerInvariant()
      if this.Pack <> defaultPack then
         dict.["pack"] <- this.Pack.ToString()
      if this.PackMode <> defaultPackMode then
         dict.["packmode"] <- this.PackMode.ToString()
      if this.Pad <> defaultPad then
         dict.["pad"] <- this.Pad.ToString()
      match this.Page with
      | Some p -> dict.["page"] <- p.ToString()
      | None -> ()
      if this.PageDir <> defaultPageDir then
         dict.["pagedir"] <- this.PageDir.ToString()

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
