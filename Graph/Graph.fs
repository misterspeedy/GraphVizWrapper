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
type Graph(id : Id, strictness: Strictness, kind : GraphKind, statements : Statement list) =
   member __.Id = id
   member __.Strictness = strictness
   member __.Kind = kind
   override this.ToString() =
      let sb = StringBuilder()
      let (~~) (text:string) = sb.Append text |> ignore
      let (~~~) (text:string) = ~~text; ~~" "
      let (~~~~) (text:string) = sb.AppendLine text |> ignore
      ~~~ this.Strictness.ToString()
      ~~~ this.Kind.ToString()
      ~~~ (this.Id.ToString() |> quote)
      ~~~~ "{"
      for statement in statements do
         ~~~~ statement.ToString()
      ~~~~ "}"
      sb.ToString().Trim()
      
