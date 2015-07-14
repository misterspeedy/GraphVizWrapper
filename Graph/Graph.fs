#if INTERACTIVE
#else
namespace GraphVizWrapper
#endif

open System.Text
open Microsoft.FSharp.Reflection

// TODO move
[<AutoOpen>]
module Utils =
   let getUnionCaseName (x:'a) = 
      match FSharpValue.GetUnionFields(x, typeof<'a>) with
      | case, _ -> case.Name  
   let quote s =
      sprintf "\"%s\"" s

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

type Node(id : Id) =
   member __.Id = id
   override this.ToString() =
      this.Id.ToString()

type EdgeType =
| Directed
| Undirected
   override this.ToString() =
      match this with
      | Directed -> "->"
      | Undirected -> "--"

type Statement =
| NodeStatement of Node
| EdgeStatement of Node * Node * EdgeType
   override this.ToString() =
      match this with
      | NodeStatement n -> 
         sprintf "%O;" n.Id
      | EdgeStatement(n1, n2, et) ->
         sprintf "%O %O %O;" n1 et n2

type AttributeStatementType =
| Graph
| Node
| Edge

type Attribute (key : Id, value : Id) =
   member __.Key = key
   member __.Value = value
   override this.ToString() =
      sprintf "%s = %s" (this.Key.ToString() |> quote) (this.Key.ToString() |> quote)

type AttributeStatement(attributeStatementType : AttributeStatementType, attributes : Attribute list) =
   member __.Attributes = attributes
   override __.ToString() =
      seq {
         yield "[ "
         for attribute in attributes do
            yield attribute.ToString()
            yield "; "
         yield "]"
      } |> Seq.reduce (+)

type Graph(id : Id, strictness: Strictness, kind : GraphKind, statements : Statement list) =
   new (id : Id, strictness: Strictness, kind : GraphKind) =
      Graph(id, strictness, kind, [])
   member __.Id = id
   member __.Strictness = strictness
   member __.Kind = kind
   member __.Statements = statements
   member this.WithStatement(statement : Statement) =
      Graph(this.Id, this.Strictness, this.Kind, statement::this.Statements)
   override this.ToString() =
      let sb = StringBuilder()
      let (~~) (text:string) = sb.Append text |> ignore
      let (~~~) (text:string) = ~~text; ~~" "
      let (~~~~) (text:string) = sb.AppendLine text |> ignore
      if this.Strictness = Strict then
         ~~~ this.Strictness.ToString()
      ~~~ this.Kind.ToString()
      ~~~ (this.Id.ToString() |> quote)
      ~~~~ "{"
      for statement in statements do
         ~~~~ statement.ToString()
      ~~~~ "}"
      sb.ToString()
