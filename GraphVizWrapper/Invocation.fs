namespace GraphVizWrapper

open System.Diagnostics
open System.IO

module Invocation =

   // TODO make configurable
   let private graphVizPath = @"C:\Program Files (x86)\Graphviz2.38\bin\"

   // Shell out to run a command line program
   let private startProcessAndCaptureOutput cmd cmdParams = 
      let si = new System.Diagnostics.ProcessStartInfo(cmd, cmdParams)
      si.UseShellExecute <- false
      si.RedirectStandardOutput <- true
      si.RedirectStandardError <- true
      use p = new System.Diagnostics.Process()
      p.StartInfo <- si
      if p.Start() then 
         use stdError = p.StandardError
         let message = stdError.ReadToEnd()
         if System.String.IsNullOrWhiteSpace message then
            let content = p.StandardOutput.ReadToEnd()
            CommandResult.Success content
         elif message.Contains "syntax error" then
            CommandResult.Failure (sprintf "Invalid input file content: %s" message)
         else 
            CommandResult.Failure (sprintf "Unspecified error: %s" message)
      else 
         use stdError = p.StandardError
         let message = stdError.ReadToEnd()
         CommandResult.Failure message

   let Call (algo : Algo, inputFilePath : string) =
      if not (File.Exists(inputFilePath)) then
         CommandResult.Failure "Missing input file"
      elif FileInfo(inputFilePath).Length = 0L then
         CommandResult.Failure "Empty input file"
      else
         let commandName = "dot"
         let commandPath = Path.ChangeExtension(Path.Combine(graphVizPath, commandName), ".exe")
         let paramString = sprintf "-Tsvg %s" inputFilePath
         startProcessAndCaptureOutput commandPath paramString
