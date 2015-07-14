namespace GraphVizWrapper

type OutputType =
| Svg
| Gif
with 
   member this.ResultFormat() =
      match this with
      | Svg -> ResultFormat.Text
      | Gif -> ResultFormat.Binary