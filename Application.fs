// Learn more about F# at http://fsharp.org

open System
open System.Reflection
open Avalonia
open Avalonia.Media
open Avalonia.Controls
open Avalonia.Markup.Xaml

open DSL

type App () =
    inherit Application ()
    override this.Initialize () =
        AvaloniaXamlLoader.Load(this)

[<EntryPoint>]
let main argv =
    AppBuilder.Configure<App>()
        .UsePlatformDetect()
        .SetupWithoutStarting() |> ignore

    let mutable textBlockValue = "Hello, world!"
    window {
        minHeight 100.0
        minWidth 100.0
        content (
            grid {
                rowDefinitions [| 20.0; 100.0 |]
                content [|
                    textBlock {
                        text textBlockValue
                        row 0
                    }
                    button {
                        text "Click"
                        click (fun f -> textBlockValue <- "Clicked")
                        row 1
                    }
                |]
            })
        title "Hello world from F# DSL"
        run
    } |> ignore

    0 // return an integer exit code