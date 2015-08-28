module ArchieToGraph

open System.IO
open Archie
open GraphVizWrapper

let ArchitectureToGraph (architecture : Architecture) : CommandResult =

    let nodes =
        architecture.Components
        |> Seq.map (fun c -> GraphNode(Id c.Name))

    let graph =
        Graph(Id "id", Strictness.NonStrict, GraphKind.Digraph)

    let graph : Graph =
        nodes |>
        Seq.fold (fun g item ->
            let node = GraphNode(item.Id)
            let ns = NodeStatement node
            let g' = g.WithStatement(ns)
            g'
        ) graph

    let edges =
        architecture
        |> Find.Edges
        |> Seq.map (fun (user, activity, provider) ->
            let n1 = GraphNode(Id user.Name)
            let n2 = GraphNode(Id provider.Name)
            EdgeStatement(activity.Name, n1, n2, Directionality.Directed)
        )

    let graph : Graph =
        edges |>
        Seq.fold (fun g es ->
            let g2 = g.WithStatement(es)
            g2
        ) graph

    // TODO somehow make sure all attributes copied on With...
    // but for now set this after adding statements
    //graph.Size <- Size.DesiredHW(5., 5.) |> Some
    //graph.Size <- Size.MaxHW(6.,6.) |> Some
    //graph.Page <- Page(7., 7.) |> Some
    graph.FontSize <- FontSize(14.)
    //graph.Splines <- Splines.Line |> Some
    graph.Overlap <- Overlap.False |> Some
    // graph.Mode <- Mode.Hier
    //graph.FontName <- "ARIALUNI.TTF"
    //graph.BgColor <- GraphColor.SingleColor(System.Drawing.Color.AliceBlue) |> Some

    GraphVizWrapper.Invocation.Call(Algo.Dot, OutputType.Png, graph.ToString())

let private GraphToFile(commandResult : CommandResult) =
    let tempFileName = Path.GetTempFileName()
    let tempFileNameExt = 
        match commandResult with
        | CommandResult.SuccessText t ->
            let tempFileSvg = Path.ChangeExtension(tempFileName, "svg")
            File.Move(tempFileName, tempFileSvg)
            File.WriteAllText(tempFileSvg, t)
            tempFileSvg
        | CommandResult.SuccessBinary b ->
            let tempFilePng = Path.ChangeExtension(tempFileName, "png")
            File.Move(tempFileName, tempFilePng)
            File.WriteAllBytes(tempFilePng, b)
            tempFilePng
        | CommandResult.Failure m ->
            failwithf "Error generating graph: %s" m
    tempFileNameExt

let ViewGraph (commandResult : CommandResult) =
    let graphFileName = GraphToFile commandResult
    let si =
        new System.Diagnostics.ProcessStartInfo(graphFileName, "",
            UseShellExecute = true,
            RedirectStandardOutput = false,
            RedirectStandardError = false,
            RedirectStandardInput = false)
    use p = new System.Diagnostics.Process()
    p.StartInfo <- si
    p.Start() |> ignore