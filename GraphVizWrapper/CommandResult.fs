namespace GraphVizWrapper

type ResultFormat =
| Text
| Binary

type CommandResult =
| SuccessText of content:string
| SuccessBinary of content:array<byte>
| Failure of message:string

