module GraphTests

open System.Drawing
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
   let emptyGraphWithDefaultDistance = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ defaultdist = 3.142 ]\
      }"
   let emptyGraphWithDim = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ dim = 3 ]\
      }"
   let emptyGraphWithDimen = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ dimen = 10 ]\
      }"
   let emptyGraphWithDirEdgeConstraintsTrue = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ diredgeconstraints = true ]\
      }"
   let emptyGraphWithDirEdgeConstraintsHier = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ diredgeconstraints = \"hier\" ]\
      }"
   let emptyGraphWithDpi = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ dpi = 300 ]\
      }"
   let emptyGraphWithEpsilon = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ epsilon = 1.2 ]\
      }"
   let emptyGraphWithESepSimpleNonAdditive = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ esep = 1.2 ]\
      }"
   let emptyGraphWithESepPointNonAdditive = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ esep = \"1.2,3.4\" ]\
      }"
   let emptyGraphWithESepSimpleAdditive = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ esep = +1.2 ]\
      }"
   let emptyGraphWithESepPointAdditive = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ esep = \"+1.2,3.4\" ]\
      }"
   let emptyGraphWithFontColor = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ fontcolor = \"PeachPuff\" ]\
      }"
   let emptyGraphWithFontName = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ fontname = \"Helvetica\" ]\
      }"
   let emptyGraphWithFontNames = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ fontnames = \"svg\" ]\
      }"
   let emptyGraphWithFontPath = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ fontpath = \"c:\\fonts\" ]\
      }"
   let emptyGraphWithFontSize = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ fontsize = 11.5 ]\
      }"
   let emptyGraphWithForceLabels = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ forcelabels = false ]\
      }"
   let emptyGraphWithGradientAngle = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ gradientangle = 180 ]\
      }"
   let emptyGraphWithId = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ id = \"id attribute\" ]\
      }"
   let emptyGraphWithImagePath = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ imagepath = \"element1;element2\" ]\
      }"
   let emptyGraphWithInputScale = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ inputscale = 36 ]\
      }"
   let emptyGraphWithLabel = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ label = \"graph label\" ]\
      }"
   let emptyGraphWithLabelSchemePenaltyCenter = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ labelscheme = 1 ]\
      }"
   let emptyGraphWithLabelSchemePenaltyOldCenter = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ labelscheme = 2 ]\
      }"
   let emptyGraphWithLabelSchemeTwoStep = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ labelscheme = 3 ]\
      }"
   let emptyGraphWithLabelJustRight = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ labeljust = \"r\" ]\
      }"
   let emptyGraphWithLabelJustLeft = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ labeljust = \"l\" ]\
      }"
   let emptyGraphWithLabelLocTop = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ labelloc = \"t\" ]\
      }"
   let emptyGraphWithLabelLocCenter = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ labelloc = \"c\" ]\
      }"
   let emptyGraphWithLabelLocBottom = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ labelloc = \"b\" ]\
      }"
   // Landscape = true is a synonym for rotation = 90
   let emptyGraphWithLandscape = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ rotation = 90 ]\
      }"
   let emptyGraphWithLayerListSep = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ layerlistsep = \";\" ]\
      }"
   let emptyGraphWithLayerSep = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ layersep = \"|\" ]\
      }"
   let emptyGraphWithLayersUsingDefaultSep = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ layers = \"layer1:layer2\" ]\
      }"
   let emptyGraphWithLayersUsingNonDefaultSep = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ layers = \"layer1|layer2\"; layersep = \"|\" ]\
      }"
   let emptyGraphWithLayerSelectSome = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ layers = \"layer1:layer2:layer3\"; \
      layerselect = \"1,3\" ]\
      }"
   let emptyGraphWithLayerSelectSomeNonDefaultLayerListSep = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ layerlistsep = \"|\"; \
      layers = \"layer1:layer2:layer3\"; \
      layerselect = \"1|3\" ]\
      }"
   let emptyGraphWithLayout = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ layout = \"neato\" ]\
      }"
   let emptyGraphWithLevels = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ levels = 99 ]\
      }"
   let emptyGraphWithLevelsGap = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ levelsgap = 0.1 ]\
      }"
   let emptyGraphWithLHeight = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ lheight = 1.2 ]\
      }"
   let emptyGraphWithLp = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ lp = \"1.2,3.4\" ]\
      }"
   let emptyGraphWithLWidth = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ lwidth = 5.6 ]\
      }"
   let emptyGraphWithSingleMargin = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ margin = 7.8 ]\
      }"
   let emptyGraphWithXYMargin = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ margin = \"7.8,9.1\" ]\
      }"
   let emptyGraphWithMaxIter = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ maxiter = 999 ]\
      }"
   let emptyGraphWithMcLimit = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ mclimit = 1.5 ]\
      }"
   let emptyGraphWithMinDist = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ mindist = 0.4 ]\
      }"
   // Major is the default
   let emptyGraphWithModeMajor = 
      "graph \"id\"\r\n\
      {\r\n\
      }"
   let emptyGraphWithModeKK = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ mode = \"KK\" ]\
      }"
   let emptyGraphWithModeHier = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ mode = \"hier\" ]\
      }"
   let emptyGraphWithModeIpSep = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ mode = \"ipsep\" ]\
      }"
   let emptyGraphWithModeSpring = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ mode = \"spring\" ]\
      }"
   let emptyGraphWithModeMaxEnt = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ mode = \"maxent\" ]\
      }"
   // Shortpath is the default
   let emptyGraphWithModelShortPath = 
      "graph \"id\"\r\n\
      {\r\n\
      }"
   let emptyGraphWithModelCircuit = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ model = \"circuit\" ]\
      }"
   let emptyGraphWithModelSubset = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ model = \"subset\" ]\
      }"
   let emptyGraphWithModelMds = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ model = \"mds\" ]\
      }"
   let emptyGraphWithMosek = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ mosek = true ]\
      }"
   let emptyGraphWithNodeSep = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ nodesep = 0.8 ]\
      }"
   let emptyGraphWithNoJustify = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ nojustify = true ]\
      }"
   let emptyGraphWithNormalizeTrue = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ normalize = true ]\
      }"
   let emptyGraphWithNormalizeNumber = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ normalize = 22.5 ]\
      }"
   let emptyGraphWithNoTranslate = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ notranslate = true ]\
      }"
   let emptyGraphWithNsLimit = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ nslimit = 3.2 ]\
      }"
   let emptyGraphWithNsLimit1 = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ nslimit1 = 5.4 ]\
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

   [<Test>]
   member __.``We can set the defaultdist attribute to a non default value``() =
      let expected = emptyGraphWithDefaultDistance
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, DefaultDistance = Some 3.142)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the dim attribute to a non default value``() =
      let expected = emptyGraphWithDim
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Dim = Dimension(3))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the dimen attribute to a non default value``() =
      let expected = emptyGraphWithDimen
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Dimen = Dimension(10))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the diredgeconstraints attribute to true``() =
      let expected = emptyGraphWithDirEdgeConstraintsTrue
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, DirEdgeConstraints = DirEdgeConstraints.True)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the diredgeconstraints attribute to 'hier'``() =
      let expected = emptyGraphWithDirEdgeConstraintsHier
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, DirEdgeConstraints = DirEdgeConstraints.Hier)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the dpi attribute to a non default value``() =
      let expected = emptyGraphWithDpi
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Dpi = 300)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the epsilon attribute to a non default value``() =
      let expected = emptyGraphWithEpsilon
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Epsilon = Some 1.2)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the esep attribute to a simple non additive value``() =
      let expected = emptyGraphWithESepSimpleNonAdditive
      let esep = Separation(1.2, false)
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, ESep = Some esep)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the esep attribute to a point non additive value``() =
      let expected = emptyGraphWithESepPointNonAdditive
      let esep = Separation(1.2, 3.4, false)
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, ESep = Some esep)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the esep attribute to a simple additive value``() =
      let expected = emptyGraphWithESepSimpleAdditive
      let esep = Separation(1.2, true)
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, ESep = Some esep)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the esep attribute to a point additive value``() =
      let expected = emptyGraphWithESepPointAdditive
      let esep = Separation(1.2, 3.4, true)
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, ESep = Some esep)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the fontcolor attribute to a non default value``() =
      let expected = emptyGraphWithFontColor
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, FontColor = Color.PeachPuff)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the fontname attribute to a non default value``() =
      let expected = emptyGraphWithFontName
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, FontName = "Helvetica")
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the fontnames attribute to a non default value``() =
      let expected = emptyGraphWithFontNames
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, FontNames = FontNames.Svg)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the fontpath attribute to a non default value``() =
      let expected = emptyGraphWithFontPath
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, FontPath = @"c:\fonts")
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the fontsize attribute to a non default value``() =
      let expected = emptyGraphWithFontSize
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, FontSize = FontSize(11.5))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the forcelabels attribute to a non default value``() =
      let expected = emptyGraphWithForceLabels
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, ForceLabels = false)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the gradientangle attribute to a non default value``() =
      let expected = emptyGraphWithGradientAngle
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, GradientAngle = 180)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the href attribute to a non default value``() =
      let expected = emptyGraphWithUrl
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Href = "www.kiteason.com")
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the id attribute to a non default value``() =
      let expected = emptyGraphWithId
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, IdAttribute = "id attribute")
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the imagepath attribute to a non default value``() =
      let expected = emptyGraphWithImagePath
      let imagePath = ImagePath([|"element1"; "element2"|])
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, ImagePath = imagePath)
      let actual = sut.ToString()
      actual |> should equal expected
   
   [<Test>]
   member __.``We can set the inputscale attribute to a non default value``() =
      let expected = emptyGraphWithInputScale
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, InputScale = 36.)
      let actual = sut.ToString()
      actual |> should equal expected
   
   [<Test>]
   member __.``We can set the label attribute to a non default value``() =
      let expected = emptyGraphWithLabel
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Label = "graph label")
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the labelscheme attribute to a value of 'penalty center' (1)``() =
      let expected = emptyGraphWithLabelSchemePenaltyCenter
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, LabelScheme = LabelScheme.PenaltyCenter)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the labelscheme attribute to a value of 'penalty old center' (2)``() =
      let expected = emptyGraphWithLabelSchemePenaltyOldCenter
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, LabelScheme = LabelScheme.PenaltyCenterOld)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the labelscheme attribute to a value of 'two step' (3)``() =
      let expected = emptyGraphWithLabelSchemeTwoStep
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, LabelScheme = LabelScheme.TwoStep)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the labeljust attribute to a value of 'right'``() =
      let expected = emptyGraphWithLabelJustRight
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, LabelJust = LabelJust.Right)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the labeljust attribute to a value of 'left'``() =
      let expected = emptyGraphWithLabelJustLeft
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, LabelJust = LabelJust.Left)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the labelloc attribute to a value of 'top'``() =
      let expected = emptyGraphWithLabelLocTop
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, LabelLoc = Some LabelLoc.Top)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the labelloc attribute to a value of 'center'``() =
      let expected = emptyGraphWithLabelLocCenter
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, LabelLoc = Some LabelLoc.Center)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the labelloc attribute to a value of 'bottom'``() =
      let expected = emptyGraphWithLabelLocBottom
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, LabelLoc = Some LabelLoc.Bottom)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the landscape attribute to true causing a rotation of 90``() =
      let expected = emptyGraphWithLandscape
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Landscape = true)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the layerlistsep attribute to a non default value``() =
      let expected = emptyGraphWithLayerListSep
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, LayerListSep = ";")
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the layers attribute to a non default value (and default layersep)``() =
      let expected = emptyGraphWithLayersUsingDefaultSep
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, 
            Layers = Layers([|Layer("layer1", true); Layer("layer2", true)|]))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the layers attribute to a non default value (and non default layersep)``() =
      let expected = emptyGraphWithLayersUsingNonDefaultSep
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, 
            LayerSep = "|", Layers = Layers([|Layer("layer1", true); Layer("layer2", true)|]))
      let actual = sut.ToString()
      actual |> should equal expected
      
   [<Test>]
   member __.``We can set the layersep attribute to a non default value``() =
      let expected = emptyGraphWithLayerSep
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, LayerSep = "|")
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the layerselect attribute to a non default value``() =
      let expected = emptyGraphWithLayerSelectSome
      let layers =
         Layers(
            [|
               Layer("layer1", true)
               Layer("layer2", false)
               Layer("layer3", true)
            |]
         )
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Layers = layers)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the layerselect attribute to a non default value (with a non default layerlistsep attribute)``() =
      let expected = emptyGraphWithLayerSelectSomeNonDefaultLayerListSep
      let layers =
         Layers(
            [|
               Layer("layer1", true)
               Layer("layer2", false)
               Layer("layer3", true)
            |]
         )
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, 
            Layers = layers, LayerListSep = "|")
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the layout attribute to a non default value``() =
      let expected = emptyGraphWithLayout
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Layout = "neato")
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the levels attribute to a non default value``() =
      let expected = emptyGraphWithLevels
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Levels = 99)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the levelsgap attribute to a non default value``() =
      let expected = emptyGraphWithLevelsGap
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, LevelsGap = 0.1)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the levelheight attribute to a non default value``() =
      let expected = emptyGraphWithLHeight
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, LHeight = 1.2)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the lp attribute to a non default value``() =
      let expected = emptyGraphWithLp
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Lp = Some(GraphPoint(1.2, 3.4)))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the lwidth attribute to a non default value``() =
      let expected = emptyGraphWithLWidth
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, LWidth = 5.6)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the margin attribute to a single non default value``() =
      let expected = emptyGraphWithSingleMargin 
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Margin = Margin(7.8))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the margin attribute to an x,y non default value``() =
      let expected = emptyGraphWithXYMargin 
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Margin = Margin(GraphPoint(7.8, 9.1)))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the maxiter attribute to a non default value``() =
      let expected = emptyGraphWithMaxIter 
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, MaxIter = 999)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the mclimit attribute to a non default value``() =
      let expected = emptyGraphWithMcLimit 
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, McLimit = 1.5)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the mindist attribute to a non default value``() =
      let expected = emptyGraphWithMinDist
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, MinDist = 0.4)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the mode attribute to a value of 'major'``() =
      let expected = emptyGraphWithModeMajor
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Mode = Mode.Major)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the mode attribute to a value of 'KK'``() =
      let expected = emptyGraphWithModeKK
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Mode = Mode.KK)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the mode attribute to a value of 'hier'``() =
      let expected = emptyGraphWithModeHier
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Mode = Mode.Hier)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the mode attribute to a value of 'ipsep'``() =
      let expected = emptyGraphWithModeIpSep
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Mode = Mode.IpSep)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the mode attribute to a value of 'spring'``() =
      let expected = emptyGraphWithModeSpring
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Mode = Mode.Spring)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the mode attribute to a value of 'maxent'``() =
      let expected = emptyGraphWithModeMaxEnt
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Mode = Mode.MaxEnt)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the model attribute to a value of 'shortpath'``() =
      let expected = emptyGraphWithModelShortPath
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Model = Model.ShortPath)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the model attribute to a value of 'circuit'``() =
      let expected = emptyGraphWithModelCircuit
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Model = Model.Circuit)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the model attribute to a value of 'subset'``() =
      let expected = emptyGraphWithModelSubset
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Model = Model.Subset)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the model attribute to a value of 'mds'``() =
      let expected = emptyGraphWithModelMds
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Model = Model.Mds)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the mosek attribute to a non default value``() =
      let expected = emptyGraphWithMosek
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Mosek = true)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the nodesep attribute to a non default value``() =
      let expected = emptyGraphWithNodeSep
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, NodeSep = NodeSep(0.8))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the nojustify attribute to a non default value``() =
      let expected = emptyGraphWithNoJustify
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, NoJustify = true)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the normalize attribute to a true``() =
      let expected = emptyGraphWithNormalizeTrue
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Normalize = Normalize.Bool(true))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the normalize attribute to a number``() =
      let expected = emptyGraphWithNormalizeNumber
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Normalize = Normalize.Degrees(22.5))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the notranslate element to a non default value``() =
      let expected = emptyGraphWithNoTranslate
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, NoTranslate = true)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the nslimit element to a non default value``() =
      let expected = emptyGraphWithNsLimit
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, NsLimit = 3.2)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the nslimit1 element to a non default value``() =
      let expected = emptyGraphWithNsLimit1
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, NsLimit1 = 5.4)
      let actual = sut.ToString()
      actual |> should equal expected

