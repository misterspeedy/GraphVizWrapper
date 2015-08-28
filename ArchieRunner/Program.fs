open ArchieToGraph
open Archie.ResourceManager

[<EntryPoint>]
let main argv = 
    resourceManager
    |> ArchitectureToGraph
    |> ViewGraph
    0 
