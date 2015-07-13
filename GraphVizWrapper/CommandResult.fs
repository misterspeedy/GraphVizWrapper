namespace GraphVizWrapper

open System.IO

type CommandResult =
| Success of content:string
| Failure of message:string

