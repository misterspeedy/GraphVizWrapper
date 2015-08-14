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
      \x20\x20[ dpi = 123.4 ]\
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
   let emptyGraphWithOrderingOut = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ ordering = \"out\" ]\
      }"
   let emptyGraphWithOrderingIn = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ ordering = \"in\" ]\
      }"
   let emptyGraphWithOrientation = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ orientation = 22.5 ]\
      }"
   // breadthfirst is the default:
   let emptyGraphWithOutputOrderBreadthFirst = 
      "graph \"id\"\r\n\
      {\r\n\
      }"
   let emptyGraphWithOutputOrderNodesFirst = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ outputorder = \"nodesfirst\" ]\
      }"
   let emptyGraphWithOutputOrderEdgesFirst = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ outputorder = \"edgesfirst\" ]\
      }"
   let emptyGraphWithOverLapTrue = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ overlap = true ]\
      }"
   let emptyGraphWithOverLapFalse = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ overlap = false ]\
      }"
   let emptyGraphWithOverLapScale = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ overlap = \"scale\" ]\
      }"
   let emptyGraphWithOverLapPrism = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ overlap = \"prism\" ]\
      }"
   let emptyGraphWithOverLapPrismSuffix = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ overlap = \"prism3\" ]\
      }"
   let emptyGraphWithOverLapVoronoi = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ overlap = \"voronoi\" ]\
      }"
   let emptyGraphWithOverLapscaleXY = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ overlap = \"scalexy\" ]\
      }"
   let emptyGraphWithOverLapCompress = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ overlap = \"compress\" ]\
      }"
   let emptyGraphWithOverLapVpsc = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ overlap = \"vpsc\" ]\
      }"
   let emptyGraphWithOverLapOrtho = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ overlap = \"ortho\" ]\
      }"
   let emptyGraphWithOverLapOrthoXY = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ overlap = \"orthoxy\" ]\
      }"
   let emptyGraphWithOverLapOrthoYX = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ overlap = \"orthoyx\" ]\
      }"
   let emptyGraphWithOverLapOrtho_YX = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ overlap = \"ortho_yx\" ]\
      }"
   let emptyGraphWithOverLapPOrtho = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ overlap = \"portho\" ]\
      }"
   let emptyGraphWithOverLapPOrthoXY = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ overlap = \"porthoxy\" ]\
      }"
   let emptyGraphWithOverLapPOrthoYX = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ overlap = \"porthoyx\" ]\
      }"
   let emptyGraphWithOverLapPOrtho_YX = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ overlap = \"portho_yx\" ]\
      }"
   let emptyGraphWithOverLapIpSep = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ overlap = \"ipsep\" ]\
      }"
   let emptyGraphWithOverLapTruePrefix = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ overlap = \"2:true\" ]\
      }"
   // -4 is the default
   let emptyGraphWithOverLapScalingMinus4 = 
      "graph \"id\"\r\n\
      {\r\n\
      }"
   let emptyGraphWithOverLapScaling = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ overlap_scaling = 3 ]\
      }"
   let emptyGraphWithOverLapShrink = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ overlap_shrink = false ]\
      }"
   // false is the default
   let emptyGraphWithPackFalse = 
      "graph \"id\"\r\n\
      {\r\n\
      }"
   let emptyGraphWithPackTrue = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ pack = true ]\
      }"
   let emptyGraphWithPackN = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ pack = 12 ]\
      }"
   // This is the default
   let emptyGraphWithPackModeNode = 
      "graph \"id\"\r\n\
      {\r\n\
      }"
   let emptyGraphWithPackModeClust = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ packmode = \"clust\" ]\
      }"
   let emptyGraphWithPackModeGraph = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ packmode = \"graph\" ]\
      }"
   let emptyGraphWithPackModeArray = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ packmode = \"array\" ]\
      }"
   let emptyGraphWithPackModeArrayC = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ packmode = \"array_c\" ]\
      }"
   let emptyGraphWithPackModeArrayCN = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ packmode = \"array_c5\" ]\
      }"
   let emptyGraphWithPackModeArrayT = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ packmode = \"array_t\" ]\
      }"
   let emptyGraphWithPackModeArrayB = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ packmode = \"array_b\" ]\
      }"
   let emptyGraphWithPackModeArrayL = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ packmode = \"array_l\" ]\
      }"
   let emptyGraphWithPackModeArrayR = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ packmode = \"array_r\" ]\
      }"
   let emptyGraphWithPackModeArrayCNR = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ packmode = \"array_c3r\" ]\
      }"
   let emptyGraphWithPackModeArrayU = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ packmode = \"array_u\" ]\
      }"
   let emptyGraphWithPackModeArrayCNRU = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ packmode = \"array_c3ru\" ]\
      }"
   let emptyGraphWithPadN = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ pad = 1.2 ]\
      }"
   let emptyGraphWithPadXY = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ pad = \"1.2,3.4\" ]\
      }"
   let emptyGraphWithPageN = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ page = 4.5 ]\
      }"
   let emptyGraphWithPageXY = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ page = \"4.5,6.7\" ]\
      }"
   // This is the default:
   let emptyGraphWithPageDirBL = 
      "graph \"id\"\r\n\
      {\r\n\
      }"
   let emptyGraphWithPageDirBR = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ pagedir = \"BR\" ]\
      }"
   let emptyGraphWithPageDirTL = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ pagedir = \"TL\" ]\
      }"
   let emptyGraphWithPageDirTR = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ pagedir = \"TR\" ]\
      }"
   let emptyGraphWithPageDirRB = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ pagedir = \"RB\" ]\
      }"
   let emptyGraphWithPageDirRT = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ pagedir = \"RT\" ]\
      }"
   let emptyGraphWithPageDirLB = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ pagedir = \"LB\" ]\
      }"
   let emptyGraphWithPageDirLT = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ pagedir = \"LT\" ]\
      }"
   let emptyGraphWithPenColorNamed = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ pencolor = \"PeachPuff\" ]\
      }"
   let emptyGraphWithPenColorArgb = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ pencolor = \"#01020304\" ]\
      }"
   let emptyGraphWithPenColorList = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ pencolor = \"PeachPuff;0.4:#01020304;0.6\" ]\
      }"
   let emptyGraphWithPenColorListNoWeighting = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ pencolor = \"PeachPuff:#01020304\" ]\
      }"
   // This is the default
   let emptyGraphWithQuadtreeNormal = 
      "graph \"id\"\r\n\
      {\r\n\
      }"
   let emptyGraphWithQuadtreeNone = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ quadtree = \"none\" ]\
      }"
   let emptyGraphWithQuadtreeFast = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ quadtree = \"fast\" ]\
      }"
   let emptyGraphWithQuantum = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ quantum = 1.23 ]\
      }"
   // This is the default:
   let emptyGraphWithRankdirTB = 
      "graph \"id\"\r\n\
      {\r\n\
      }"
   let emptyGraphWithRankdirLR = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ rankdir = \"LR\" ]\
      }"
   let emptyGraphWithRankdirBT = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ rankdir = \"BT\" ]\
      }"
   let emptyGraphWithRankdirRL = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ rankdir = \"RL\" ]\
      }"
   let emptyGraphWithRankSepOne = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ ranksep = 1.2 ]\
      }"
   let emptyGraphWithRankSepSeveral = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ ranksep = \"1.2:3.4:5.6\" ]\
      }"
   let emptyGraphWithRankSepOneEqually = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ ranksep = \"1.2 equally\" ]\
      }"
   let emptyGraphWithRatioN = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ ratio = 1.2 ]\
      }"
   let emptyGraphWithRatioFill = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ ratio = \"fill\" ]\
      }"
   let emptyGraphWithRatioCompress = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ ratio = \"compress\" ]\
      }"
   let emptyGraphWithRatioExpand = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ ratio = \"expand\" ]\
      }"
   let emptyGraphWithRatioAuto = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ ratio = \"auto\" ]\
      }"
   let emptyGraphWithReMinCross = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ remincross = false ]\
      }"
   let emptyGraphWithRepulsiveForce = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ repulsiveforce = 1.9 ]\
      }"
   // Resolution is a synonym for DPI
   let emptyGraphWithResolution = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ dpi = 456.7 ]\
      }"
   let emptyGraphWithRoot = 
      "graph \"id\"\r\n\
      {\r\n\
      \x20\x20[ root = \"node99\" ]\
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
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Dpi = 123.4)
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
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, FontColor = FontColor(Color.PeachPuff))
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
            Layers = Layers([|Layer("layer1", true); Layer("layer2", true)|], "|"))
      let actual = sut.ToString()
      actual |> should equal expected
      
   [<Test>]
   member __.``We can set the layersep attribute to a non default value``() =
      let expected = emptyGraphWithLayerSep
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph)
      sut.Layers.LayerSep <- "|"
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

   [<Test>]
   member __.``We can set the ordering element to 'out'``() =
      let expected = emptyGraphWithOrderingOut
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Ordering = Some(Ordering.Out))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the ordering element to 'in'``() =
      let expected = emptyGraphWithOrderingIn
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Ordering = Some(Ordering.In))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the orientation element to a non default value``() =
      let expected = emptyGraphWithOrientation
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Orientation = 22.5)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the outputorder element to 'breadthfirst'``() =
      let expected = emptyGraphWithOutputOrderBreadthFirst
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, OutputOrder = OutputOrder.BreadthFirst)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the outputorder element to 'nodesfirst'``() =
      let expected = emptyGraphWithOutputOrderNodesFirst
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, OutputOrder = OutputOrder.NodesFirst)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the outputorder element to 'edgesfirst``() =
      let expected = emptyGraphWithOutputOrderEdgesFirst
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, OutputOrder = OutputOrder.EdgesFirst)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the overlap element to 'true'``() =
      let expected = emptyGraphWithOverLapTrue
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Overlap = Some Overlap.True)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the overlap element to 'false'``() =
      let expected = emptyGraphWithOverLapFalse
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Overlap = Some Overlap.False)
      let actual = sut.ToString()
      actual |> should equal expected
 
   [<Test>]
   member __.``We can set the overlap element to 'scale'``() =
      let expected = emptyGraphWithOverLapScale
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Overlap = Some Overlap.Scale)
      let actual = sut.ToString()
      actual |> should equal expected
 
   [<Test>]
   member __.``We can set the overlap element to 'prism'``() =
      let expected = emptyGraphWithOverLapPrism
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Overlap = Some Overlap.Prism)
      let actual = sut.ToString()
      actual |> should equal expected
 
   [<Test>]
   member __.``We can set the overlap element to 'prism3'``() =
      let expected = emptyGraphWithOverLapPrismSuffix
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Overlap = Some (Overlap.PrismN(3)))
      let actual = sut.ToString()
      actual |> should equal expected
 
   [<Test>]
   member __.``We can set the overlap element to 'voronoi'``() =
      let expected = emptyGraphWithOverLapVoronoi
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Overlap = Some Overlap.Voronoi)
      let actual = sut.ToString()
      actual |> should equal expected
 
   [<Test>]
   member __.``We can set the overlap element to 'scalexy``() =
      let expected = emptyGraphWithOverLapscaleXY
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Overlap = Some Overlap.ScaleXY)
      let actual = sut.ToString()
      actual |> should equal expected
 
   [<Test>]
   member __.``We can set the overlap element to 'compress'``() =
      let expected = emptyGraphWithOverLapCompress
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Overlap = Some Overlap.Compress)
      let actual = sut.ToString()
      actual |> should equal expected
  
   [<Test>]
   member __.``We can set the overlap element to 'vpsc'``() =
      let expected = emptyGraphWithOverLapVpsc
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Overlap = Some Overlap.Vpsc)
      let actual = sut.ToString()
      actual |> should equal expected
 
   [<Test>]
   member __.``We can set the overlap element to 'ortho'``() =
      let expected = emptyGraphWithOverLapOrtho
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Overlap = Some Overlap.Ortho)
      let actual = sut.ToString()
      actual |> should equal expected
  
   [<Test>]
   member __.``We can set the overlap element to 'orthoxy'``() =
      let expected = emptyGraphWithOverLapOrthoXY
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Overlap = Some Overlap.OrthoXY)
      let actual = sut.ToString()
      actual |> should equal expected
  
   [<Test>]
   member __.``We can set the overlap element to 'orthoyx'``() =
      let expected = emptyGraphWithOverLapOrthoYX
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Overlap = Some Overlap.OrthoYX)
      let actual = sut.ToString()
      actual |> should equal expected
  
   [<Test>]
   member __.``We can set the overlap element to 'ortho_yx'``() =
      let expected = emptyGraphWithOverLapOrtho_YX
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Overlap = Some Overlap.Ortho_YX)
      let actual = sut.ToString()
      actual |> should equal expected
  
   [<Test>]
   member __.``We can set the overlap element to 'portho'``() =
      let expected = emptyGraphWithOverLapPOrtho
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Overlap = Some Overlap.POrtho)
      let actual = sut.ToString()
      actual |> should equal expected
  
   [<Test>]
   member __.``We can set the overlap element to 'porthoxy'``() =
      let expected = emptyGraphWithOverLapPOrthoXY
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Overlap = Some Overlap.POrthoXY)
      let actual = sut.ToString()
      actual |> should equal expected
 
   [<Test>]
   member __.``We can set the overlap element to 'porthoyx'``() =
      let expected = emptyGraphWithOverLapPOrthoYX
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Overlap = Some Overlap.POrthoYX)
      let actual = sut.ToString()
      actual |> should equal expected
  
   [<Test>]
   member __.``We can set the overlap element to 'portho_yx'``() =
      let expected = emptyGraphWithOverLapPOrtho_YX
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Overlap = Some Overlap.POrtho_YX)
      let actual = sut.ToString()
      actual |> should equal expected
  
   [<Test>]
   member __.``We can set the overlap element to 'ipsep'``() =
      let expected = emptyGraphWithOverLapIpSep
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Overlap = Some Overlap.IpSep)
      let actual = sut.ToString()
      actual |> should equal expected
  
   [<Test>]
   member __.``We can set the overlap element to '2:true'``() =
      let expected = emptyGraphWithOverLapTruePrefix
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Overlap = Some Overlap.True, OverlapPrefix = 2)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the overlap_scaling element to the default``() =
      let expected = emptyGraphWithOverLapScalingMinus4
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, OverlapScaling = -4.0)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the overlap_scaling element to a non default value``() =
      let expected = emptyGraphWithOverLapScaling
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, OverlapScaling = 3.0)
      let actual = sut.ToString()
      actual |> should equal expected
  
   [<Test>]
   member __.``We can set the overlap_shrink element to a non default value``() =
      let expected = emptyGraphWithOverLapShrink
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, OverlapShrink = false)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pack element to false``() =
      let expected = emptyGraphWithPackFalse
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Pack = Pack.False)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pack element to true with no specified margin``() =
      let expected = emptyGraphWithPackTrue
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Pack = Pack.True(None))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pack element to true with a specified margin``() =
      let expected = emptyGraphWithPackN
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Pack = Pack.True(Some 12))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pack mode element to 'node'``() =
      let expected = emptyGraphWithPackModeNode
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, PackMode = PackMode.Node)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pack mode element to 'clust'``() =
      let expected = emptyGraphWithPackModeClust
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, PackMode = PackMode.Clust)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pack mode element to 'graph'``() =
      let expected = emptyGraphWithPackModeGraph
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, PackMode = PackMode.Graph)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pack mode element to 'array'``() =
      let expected = emptyGraphWithPackModeArray
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, 
            PackMode = PackMode.Array(PackModeOrdering.Default, 
                                      PackModeAlignment.Default,
                                      PackModeInsertionOrder.Default))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pack mode element to 'array_c'``() =
      let expected = emptyGraphWithPackModeArrayC
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, 
            PackMode = PackMode.Array(PackModeOrdering.ColumnMajor, 
                                      PackModeAlignment.Default,
                                      PackModeInsertionOrder.Default))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pack mode element to 'array_c5'``() =
      let expected = emptyGraphWithPackModeArrayCN
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, 
            PackMode = PackMode.Array(PackModeOrdering.ColumnMajorN(5), 
                                      PackModeAlignment.Default,
                                      PackModeInsertionOrder.Default))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pack mode element to 'array_t'``() =
      let expected = emptyGraphWithPackModeArrayT
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, 
            PackMode = PackMode.Array(PackModeOrdering.Default, 
                                      PackModeAlignment.Top,
                                      PackModeInsertionOrder.Default))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pack mode element to 'array_b'``() =
      let expected = emptyGraphWithPackModeArrayB
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, 
            PackMode = PackMode.Array(PackModeOrdering.Default, 
                                      PackModeAlignment.Bottom,
                                      PackModeInsertionOrder.Default))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pack mode element to 'array_l'``() =
      let expected = emptyGraphWithPackModeArrayL
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, 
            PackMode = PackMode.Array(PackModeOrdering.Default, 
                                      PackModeAlignment.Left,
                                      PackModeInsertionOrder.Default))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pack mode element to 'array_r'``() =
      let expected = emptyGraphWithPackModeArrayR
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, 
            PackMode = PackMode.Array(PackModeOrdering.Default, 
                                      PackModeAlignment.Right,
                                      PackModeInsertionOrder.Default))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pack mode element to 'array_c3r'``() =
      let expected = emptyGraphWithPackModeArrayCNR
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, 
            PackMode = PackMode.Array(PackModeOrdering.ColumnMajorN(3), 
                                      PackModeAlignment.Right,
                                      PackModeInsertionOrder.Default))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pack mode element to 'array_u'``() =
      let expected = emptyGraphWithPackModeArrayU
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, 
            PackMode = PackMode.Array(PackModeOrdering.Default,
                                      PackModeAlignment.Default,
                                      PackModeInsertionOrder.User))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pack mode element to 'array_c3ru'``() =
      let expected = emptyGraphWithPackModeArrayCNRU
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, 
            PackMode = PackMode.Array(PackModeOrdering.ColumnMajorN(3),
                                      PackModeAlignment.Right,
                                      PackModeInsertionOrder.User))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pad element to a single value``() =
      let expected = emptyGraphWithPadN
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Pad = Pad(1.2))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pad element to a pair of values``() =
      let expected = emptyGraphWithPadXY
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Pad = Pad(GraphPoint(1.2, 3.4)))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the page element to a single value``() =
      let expected = emptyGraphWithPageN
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Page = (Page(4.5) |> Some))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the page element to a pair of values``() =
      let expected = emptyGraphWithPageXY
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Page = (Page(GraphPoint(4.5, 6.7)) |> Some))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pagedir element to 'BL'``() =
      let expected = emptyGraphWithPageDirBL
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, PageDir = PageDir.BottomLeft)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pagedir element to 'BR'``() =
      let expected = emptyGraphWithPageDirBR
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, PageDir = PageDir.BottomRight)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pagedir element to 'TL'``() =
      let expected = emptyGraphWithPageDirTL
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, PageDir = PageDir.TopLeft)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pagedir element to 'TR'``() =
      let expected = emptyGraphWithPageDirTR
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, PageDir = PageDir.TopRight)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pagedir element to 'RB'``() =
      let expected = emptyGraphWithPageDirRB
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, PageDir = PageDir.RightBottom)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pagedir element to 'RT'``() =
      let expected = emptyGraphWithPageDirRT
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, PageDir = PageDir.RightTop)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pagedir element to 'LB'``() =
      let expected = emptyGraphWithPageDirLB
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, PageDir = PageDir.LeftBottom)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pagedir element to 'LT'``() =
      let expected = emptyGraphWithPageDirLT
      let sut = 
         Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, PageDir = PageDir.LeftTop)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pencolor attribute to a non default value using a color name``() =
      let expected = emptyGraphWithPenColorNamed
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, PenColor = GraphColor.SingleColor(Color.PeachPuff))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pencolor attribute to a non default value using an ARGB value``() =
      let expected = emptyGraphWithPenColorArgb
      let color = GraphColor.SingleColor(Color.FromArgb(1, 2, 3, 4))
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, PenColor = color)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pencolor attribute to a color list value``() =
      let expected = emptyGraphWithPenColorList
      let color1 = { WColor = Color.PeachPuff; Weight = Some 0.4 }
      let color2 = { WColor = Color.FromArgb(1, 2, 3, 4); Weight = Some 0.6 }
      let colors = 
         WeightedColorList()
            .Add(color1)
            .Add(color2)
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, PenColor = GraphColor.ColorList(colors))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the pencolor attribute to a color list value without weightings``() =
      let expected = emptyGraphWithPenColorListNoWeighting
      let color1 = { WColor = Color.PeachPuff; Weight = None }
      let color2 = { WColor = Color.FromArgb(1, 2, 3, 4); Weight = None }
      let colors = 
         WeightedColorList()
            .Add(color1)
            .Add(color2)
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, PenColor = GraphColor.ColorList(colors))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the quadtree attribute to the default``() =
      let expected = emptyGraphWithQuadtreeNormal
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Quadtree = Quadtree.Normal)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the quadtree attribute to Fast``() =
      let expected = emptyGraphWithQuadtreeFast
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Quadtree = Quadtree.Fast)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the quadtree attribute to None``() =
      let expected = emptyGraphWithQuadtreeNone
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Quadtree = Quadtree.None_)
      let actual = sut.ToString()
      actual |> should equal expected


   [<Test>]
   member __.``We can set the quantum attribute to a non default value``() =
      let expected = emptyGraphWithQuantum
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Quantum = 1.23)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the rankDir attribute to 'TB'``() =
      let expected = emptyGraphWithRankdirTB
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, RankDir = RankDir.TopBottom)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the rankDir attribute to 'LR'``() =
      let expected = emptyGraphWithRankdirLR
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, RankDir = RankDir.LeftRight)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the rankDir attribute to 'BT'``() =
      let expected = emptyGraphWithRankdirBT
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, RankDir = RankDir.BottomTop)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the rankDir attribute to 'RL'``() =
      let expected = emptyGraphWithRankdirRL
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, RankDir = RankDir.RightLeft)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the rankSep attribute to a single value``() =
      let expected = emptyGraphWithRankSepOne
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, RankSep = Some(RankSep.Single(1.2)))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the rankSep attribute to a list of values``() =
      let expected = emptyGraphWithRankSepSeveral
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, RankSep = Some(RankSep.List(DoubleList([|1.2;3.4;5.6|]))))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the rankSep attribute to a value using 'equally'``() =
      let expected = emptyGraphWithRankSepOneEqually
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, RankSep = Some(RankSep.Equally(1.2)))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the ratio attribute to a numeric value``() =
      let expected = emptyGraphWithRatioN
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Ratio = Some(Ratio.Numeric(1.2)))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the ratio attribute to a value of 'fill'``() =
      let expected = emptyGraphWithRatioFill
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Ratio = Some(Ratio.Fill))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the ratio attribute to a value of 'compress'``() =
      let expected = emptyGraphWithRatioCompress
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Ratio = Some(Ratio.Compress))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the ratio attribute to a value of 'expand'``() =
      let expected = emptyGraphWithRatioExpand
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Ratio = Some(Ratio.Expand))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the ratio attribute to a value of 'auto'``() =
      let expected = emptyGraphWithRatioAuto
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Ratio = Some(Ratio.Auto))
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the remincross attribute a non default value``() =
      let expected = emptyGraphWithReMinCross
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, ReMinCross=false)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the repulsiveforce attribute a non default value``() =
      let expected = emptyGraphWithRepulsiveForce
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, RepulsiveForce = 1.9)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the resolution attribute a non default value``() =
      let expected = emptyGraphWithResolution
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Resolution = 456.7)
      let actual = sut.ToString()
      actual |> should equal expected

   [<Test>]
   member __.``We can set the root attribute a non default value``() =
      let expected = emptyGraphWithRoot
      let sut = Graph(Id "id", Strictness.NonStrict, GraphKind.Graph, Root = "node99")
      let actual = sut.ToString()
      actual |> should equal expected



