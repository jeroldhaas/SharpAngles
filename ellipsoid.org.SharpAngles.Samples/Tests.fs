namespace ellipsoid.org.SharpAngles.Samples

open ellipsoid.org.SharpAngles
open IntelliFactory.WebSharper

[<JavaScript>]
module Tests =

    let TestsApp = 
        Angular.Module("testsApp", [||])
               .Config(("$resource",
                            fun (resource: Resource.ResourceFactory<string>) -> 
                                ()
    ))