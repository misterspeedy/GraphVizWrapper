module InvocationTests

open System.IO
open NUnit.Framework
open FsUnit
open GraphVizWrapper

[<TestFixture>]
type InvocationTests() =

   let createFile content =
      let name = System.IO.Path.GetTempFileName()
      File.WriteAllText(name, content)
      name

   [<Test>]
   member __.``Invoking the dot command with empty input causes an 'Empty input' error``() =
      let expected = CommandResult.Failure "Empty input"
      let actual = GraphVizWrapper.Invocation.Call(Algo.Dot, "")
      actual |> should equal expected

   [<Test>]
   member __.``Invoking the dot command with null input causes an 'Null input' error``() =
      let expected = CommandResult.Failure "Null input"
      let actual = GraphVizWrapper.Invocation.Call(Algo.Dot, null)
      actual |> should equal expected

   [<Test>]
   member __.``Invoking the dot command with invalid input causes a 'Invalid input file content' error``() =
      let expected = "Invalid input content: "
      let actual = 
         match GraphVizWrapper.Invocation.Call(Algo.Dot, "invalid") with
         | Success content -> content
         | Failure message -> message
      actual |> should startWith expected

   [<TestCase("graph {}")>]
   [<TestCase("digraph {}")>]
   member __.``Invoking the dot for svg command with a file containing a valid but empty graph generates an SVG file representing a blank page``(dotContent) =
      let expected = File.ReadAllText(@"..\..\TestFiles\valid-dot-no-content.svg")
      let actual = 
         match GraphVizWrapper.Invocation.Call(Algo.Dot, dotContent) with
         | Success content -> content
         | Failure _ -> ""
      actual |> should equal expected

   [<TestCase("graph { a; }")>]
   [<TestCase("digraph { a; }")>]
   member __.``Invoking the dot for svg command with a file containing a single node generates an SVG file representing that node``(dotContent) =
      let expected = File.ReadAllText(@"..\..\TestFiles\valid-dot-one-node.svg")
      let actual = 
         match GraphVizWrapper.Invocation.Call(Algo.Dot, dotContent) with
         | Success content -> content
         | Failure message -> message
      actual |> should equal expected

   [<TestCase("graph {}")>]
   [<TestCase("digraph {}")>]
   member __.``Invoking the neato for svg command with a file containing a valid but empty graph generates an SVG file representing a blank page``(dotContent) =
      let expected = File.ReadAllText(@"..\..\TestFiles\valid-neato-no-content.svg")
      let actual = 
         match GraphVizWrapper.Invocation.Call(Algo.Neato, dotContent) with
         | Success content -> content
         | Failure _ -> ""
      actual |> should equal expected

   [<TestCase("graph { a; }")>]
   [<TestCase("digraph { a; }")>]
   member __.``Invoking the neato for svg command with a file containing a single node generates an SVG file representing that node``(dotContent) =
      let expected = File.ReadAllText(@"..\..\TestFiles\valid-neato-one-node.svg")
      let actual = 
         match GraphVizWrapper.Invocation.Call(Algo.Neato, dotContent) with
         | Success content -> content
         | Failure message -> message
      actual |> should equal expected


