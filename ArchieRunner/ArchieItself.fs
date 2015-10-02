module Archie.Itself

open Archie

module GraphViz =

    let dot = Activity.New("Create DOT graph" , Command, ShellToExe)

module Dsl =

    let dsl = Activity.New("DSL", Command, InProcess)

module GraphVizWrapper = 

    let wrapper = Activity.New("Graph.ToString()", Command, InProcess)

module Components = 

    let graphViz =
        Component("GraphViz", Processor, Singleton)
            .Provides(GraphViz.dot)

    let graphVizWrapper =
        Component("GraphVizWrapper", Processor, Singleton)
            .Provides(GraphVizWrapper.wrapper)
            .Uses(GraphViz.dot)

    let dsl = 
        Component("DSL", Processor, Singleton)
            .Provides(Dsl.dsl)
            .Uses(GraphVizWrapper.wrapper)

    let archieItself =
        Component("Archie", Processor, Singleton)
            .Uses(Dsl.dsl)

open Components

let archie = 
    {
        Name = "Archie Architecture"
        Components = 
            [
                archieItself
                dsl
                graphVizWrapper
                graphViz
            ]
    }
