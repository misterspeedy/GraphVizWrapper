open ArchieToGraph
open Archie.ResourceManager2
open Archie.Itself

[<EntryPoint>]
let main argv = 
    // resourceManager
    archie
    |> ArchitectureToGraph
    |> ViewGraph
    0 
