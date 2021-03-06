SharpAngles
===========

WebSharper bindings for Angular and Angular UI

## Overview

Experimental WebSharper bindings for Angular JS/UI plus sample project showing their implementation based on the examples
available on the [Angular website][1]. SharpAngles makes it possible to construct single page apps entirely in F#. Bindings
are based on the [DefinitelyTyped Angular definitions][2] for TypeScript. Angular modules currently implemented:

* angular: **partially**
* angular-animate: **no**
* angular-cookies: **no**
* angular-mocks: **partially**
* angular-resource: **partially**
* angular-route: **yes**
* angular-sanitize: **no**
* angular-scenario: **no**

* angular-ui-router: **yes**
* angular-ui-anythingelse: **no**

## Usage

The general idea is to implement Angular in a manner that matches the regular JavaScript usage as closely as possible.
Below is an example of the first (very simple) example found on the Angular website, translated into F#:

```F#
module Server =

    type Todo =
        { text: string
          ``done``: bool }

    [<Rpc>]
    let GetTodos () = [|
        { text = "learn angular"; ``done`` = true }
        { text = "build an angular app"; ``done`` = false }
    |]
    
[<JavaScript>]
module Client =
    open Server
    
    [<AbstractClass>]
    type TodoScope =
        inherit Scope
        [<DefaultValue>] val mutable todos: Server.Todo array
        [<DefaultValue>] val mutable addTodo: unit -> unit
        [<DefaultValue>] val mutable remaining: unit -> int
        [<DefaultValue>] val mutable archive: unit -> unit
        [<DefaultValue>] val mutable todoText: string
        
    let Main =
        Angular
            .Module("todoApp", [||])
            .Controller("TodoController", (
                            "$scope", 
                            fun (scope: TodoScope) -> 
                                scope.todos <- Server.GetTodos ()
                                scope.addTodo <-
                                    fun _ ->
                                        scope.todos.ToEcma().Push({ text = scope.todoText; ``done`` = false }) |> ignore
                                        scope.todoText <- ""
                                scope.remaining <-
                                    fun _ ->
                                        scope.todos
                                        |> Array.filter (fun t -> not t.``done``)
                                        |> Array.length
                                scope.archive <-
                                    fun _ ->
                                        scope.todos <-
                                            scope.todos
                                            |> Array.filter (fun t -> not t.``done``)
            ))
```

For further details and examples, see the sample project.

## Random Helpful Information

* When using $resource as a dependency, a ResourceFactory object is returned, featuring a 'Create' method. The 'Create' method
  is equivalent to using the $resource object, i.e. $resource('http://www.test.com/api/test') === 
  resource.Create("http://www.test.com/api/test")

## Licensing

Please bear in mind that [WebSharper][3] is subject to separate licensing for all non-open-source projects.

[1]: http://angularjs.com/
[2]: https://github.com/borisyankov/DefinitelyTyped/tree/master/angularjs
[3]: http://www.websharper.com/