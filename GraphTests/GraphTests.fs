module GraphTests

open System.IO
open NUnit.Framework
open FsUnit
open GraphVizWrapper

[<AutoOpen>]
module __ =
   let emptyGraph = 
      "graph \"id\"\r\n\
      {\r\n\
      }"
   let emptyDigraph =
      "digraph \"id\"\r\n\
      {\r\n\
      }"
   let emptyStrictGraph =
      "strict graph \"id\"\r\n\
      {\r\n\
      }"
   let oneNodeGraph =
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20\"node1\"\r\n\
      }"
   let twoNodeGraph =
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20\"node1\";\r\n\
      \x20\x20\"node2\"\r\n\
      }"
   let oneEdgeGraph =
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20\"node1\" -- \"node2\"\r\n\
      }"
   let twoEdgeGraph =
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20\"node1\" -- \"node2\";\r\n\
      \x20\x20\"node3\" -- \"node4\"\r\n\
      }"
   let twoEdgeDigraph =
      "digraph \"id\"\r\n\
      {\r\n\
      \x20\x20\"node1\" -> \"node2\";\r\n\
      \x20\x20\"node3\" -> \"node4\"\r\n\
      }"

[<TestFixture>]
type GraphTests() =

   [<Test>]
   member __.``An empty graph has the correct source``() =
      let expected = emptyGraph
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``Any empty digraph has the correct source``() =
      let expected = emptyDigraph
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Digraph)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``An empty strict graph has the correct source``() =
      let expected = emptyStrictGraph
      let sut = Graph(Id "id", Strictness.Strict, GraphKind.Graph)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``A one-node graph has the correct source``() =
      let expected = oneNodeGraph
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph)
            .WithStatement(NodeStatement(GraphNode(Id "node1")))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``A two-node graph has the correct source``() =
      let expected = twoNodeGraph
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph)
            .WithStatement(NodeStatement(GraphNode(Id "node1")))
            .WithStatement(NodeStatement(GraphNode(Id "node2")))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``A one-edge graph has the correct source``() =
      let expected = oneEdgeGraph
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph)
            .WithStatement(EdgeStatement(GraphNode(Id "node1"), GraphNode(Id "node2"), Directionality.Undirected))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``A two-edge graph has the correct source``() =
      let expected = twoEdgeGraph
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph)
            .WithStatement(EdgeStatement(GraphNode(Id "node1"), GraphNode(Id "node2"), Directionality.Undirected))
            .WithStatement(EdgeStatement(GraphNode(Id "node3"), GraphNode(Id "node4"), Directionality.Undirected))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``A two-edge directed graph has the correct source``() =
      let expected = twoEdgeDigraph
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Digraph)
            .WithStatement(EdgeStatement(GraphNode(Id "node1"), GraphNode(Id "node2"), Directionality.Directed))
            .WithStatement(EdgeStatement(GraphNode(Id "node3"), GraphNode(Id "node4"), Directionality.Directed))
      let actual = sut.ToString()
      actual |> should equal expected