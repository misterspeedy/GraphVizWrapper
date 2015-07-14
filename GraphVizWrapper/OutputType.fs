namespace GraphVizWrapper

type OutputType =
| Svg
| Gif
with 
   member this.ToResultFormat() =
      match this with
      | Svg -> ResultFormat.Text
      | Gif -> ResultFormat.Binary