open ArchieToGraph
//open Archie.ResourceManagerSpark
open Archie.Itself
//open Archie.Optout

[<EntryPoint>]
let main argv = 
    archie
    |> ArchitectureToGraph
    |> ViewGraph
    0 
