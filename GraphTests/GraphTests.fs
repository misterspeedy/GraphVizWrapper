module GraphTests

open System.IO
open NUnit.Framework
open FsUnit
open GraphVizWrapper
open System.Drawing

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
   let emptyGraphWithDamping = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ Damping = 0.5 ]\
      }"
   let emptyGraphWithK = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ K = 1.1 ]\
      }"
   let emptyGraphWithDampingAndK = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ Damping = 0.5; K = 1.1 ]\
      }"
   let emptyGraphWithUrl = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ URL = \"www.kiteason.com\" ]\
      }"
   let emptyGraphWithBackground = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ _background = \"background xdot\" ]\
      }"
   let emptyGraphWithBb = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ bb = \"0,1.11,2.222,3.3333\" ]\
      }"
   let emptyGraphWithBgColorNamed = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ bgcolor = \"PeachPuff\" ]\
      }"
   let emptyGraphWithBgColorArgb = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ bgcolor = \"#01020304\" ]\
      }"
   let emptyGraphWithBgColorList = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ bgcolor = \"PeachPuff;0.4:#01020304;0.6\" ]\
      }"
   let emptyGraphWithBgColorListNoWeighting = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ bgcolor = \"PeachPuff:#01020304\" ]\
      }"
   let emptyGraphWithCenter = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ center = true ]\
      }"
   let emptyGraphWithCharset = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ charset = \"UTF-16\" ]\
      }"
   let emptyGraphWithClusterRank = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ clusterrank = \"global\" ]\
      }"
   let emptyGraphWithColorNamed = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ color = \"PeachPuff\" ]\
      }"
   let emptyGraphWithColorArgb = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ color = \"#01020304\" ]\
      }"
   let emptyGraphWithColorList = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ color = \"PeachPuff;0.4:#01020304;0.6\" ]\
      }"
   let emptyGraphWithColorScheme = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ colorscheme = \"color scheme\" ]\
      }"
   let emptyGraphWithComment = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ comment = \"this is a comment\" ]\
      }"
   let emptyGraphWithCompound = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ compound = true ]\
      }"
   let emptyGraphWithConcentrate = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ concentrate = true ]\
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

   [<Test>]
   member __.``We can set the Damping attribute to a non default value``() =
      let expected = emptyGraphWithDamping
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Damping = 0.5)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the Damping attribute to its default value and it is not included in the source``() =
      let expected = emptyGraph
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Damping = 0.99)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the K attribute to a non default value``() =
      let expected = emptyGraphWithK
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, K = 1.1)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the Damping attribute and the K attribute to non default values``() =
      let expected = emptyGraphWithDampingAndK
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, K = 1.1, Damping = 0.5)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the Url attribute to a non default value``() =
      let expected = emptyGraphWithUrl
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Url = "www.kiteason.com")
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the _background attribute to a non default value``() =
      let expected = emptyGraphWithBackground
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Background = "background xdot")
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the Bb attribute to a non default value``() =
      let expected = emptyGraphWithBb  
      let bb = { llx = 0.0; lly = 1.11; urx = 2.222; ury = 3.3333 }
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Bb = Some bb)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the bgcolor attribute to a non default value using a color name``() =
      let expected = emptyGraphWithBgColorNamed
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, BgColor = Some(GraphColor.SingleColor(Color.PeachPuff)))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the bgcolor attribute to a non default value using an ARGB value``() =
      let expected = emptyGraphWithBgColorArgb
      let color = GraphColor.SingleColor(Color.FromArgb(1, 2, 3, 4))
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, BgColor = Some(color))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the bgcolor attribute to a color list value``() =
      let expected = emptyGraphWithBgColorList
      let color1 = { WColor = Color.PeachPuff; Weight = Some 0.4 }
      let color2 = { WColor = Color.FromArgb(1, 2, 3, 4); Weight = Some 0.6 }
      let colors = 
         WeightedColorList()
            .Add(color1)
            .Add(color2)
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, BgColor = Some(GraphColor.ColorList(colors)))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the bgcolor attribute to a color list value without weightings``() =
      let expected = emptyGraphWithBgColorListNoWeighting
      let color1 = { WColor = Color.PeachPuff; Weight = None }
      let color2 = { WColor = Color.FromArgb(1, 2, 3, 4); Weight = None }
      let colors = 
         WeightedColorList()
            .Add(color1)
            .Add(color2)
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, BgColor = Some(GraphColor.ColorList(colors)))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the center attribute to a non default value``() =
      let expected = emptyGraphWithCenter
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Center = true)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the charset attribute to a non default value``() =
      let expected = emptyGraphWithCharset
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Charset = "UTF-16")
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the clusterrank attribute to a non default value``() =
      let expected = emptyGraphWithClusterRank
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, ClusterRank = "global")
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the color attribute to a non default value using a color name``() =
      let expected = emptyGraphWithColorNamed
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Color = GraphColor.SingleColor(Color.PeachPuff))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the color attribute to a non default value using an ARGB value``() =
      let expected = emptyGraphWithColorArgb
      let color = GraphColor.SingleColor(Color.FromArgb(1, 2, 3, 4))
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Color = color)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the color attribute to a color list value``() =
      let expected = emptyGraphWithColorList
      let color1 = { WColor = Color.PeachPuff; Weight = Some 0.4 }
      let color2 = { WColor = Color.FromArgb(1, 2, 3, 4); Weight = Some 0.6 }
      let colors = 
         WeightedColorList()
            .Add(color1)
            .Add(color2)
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Color = GraphColor.ColorList(colors))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the colorscheme attribute to a non default value``() =
      let expected = emptyGraphWithColorScheme
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, ColorScheme = "color scheme")
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the comment attribute to a non default value``() =
      let expected = emptyGraphWithComment
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Comment = "this is a comment")
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the compound attribute to a non default value``() =
      let expected = emptyGraphWithCompound
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Compound = true)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the concentrate attribute to a non default value``() =
      let expected = emptyGraphWithConcentrate
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Concentrate = true)
      let actual = sut.ToString()
      actual |> should equal expected
