#if INTERACTIVE
#else
namespace GraphVizWrapper
#endif

open System
open Microsoft.FSharp.Reflection
open System.Collections.Generic

[<AutoOpen>]
module __ =
   let getUnionCaseName (x:'a) = 
      match FSharpValue.GetUnionFields(x, typeof<'a>) with
      | case, _ -> case.Name  
   let isNumber s =
      match Double.TryParse(s) with
      | true, _ -> true
      | _ -> false
   let quote s =
      sprintf "\"%s\"" s
   let dictToAttrList (dict : Dictionary<string, string>) =
      let pairs = 
         if dict.Count > 0 then
            dict
            |> Seq.map (fun kvp -> 
               let valueStr =
                  if kvp.Value |> isNumber then
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
   member private this.GraphAttributes =
      let dict = Dictionary<string, string>()
      if this.Damping <> defaultDamping then
         dict.["Damping"] <- this.Damping.ToString()
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
