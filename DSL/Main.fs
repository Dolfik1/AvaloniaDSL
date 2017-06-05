module DSL

open Avalonia
open Avalonia.Controls
open Avalonia.Media

type WindowContentBuilder() =
    member this.Yield(item: 'a): Window = Window()

    [<CustomOperation("content")>]
    member this.Content (acc: Window, value: IControl) =
        acc.Content <- box value
        acc

type WindowBuilder() =
    inherit WindowContentBuilder()

    member this.Yield(item: 'a): Window = Window()

    [<CustomOperation("minWidth")>]
    member this.MinWidth (acc: Window, value) = 
        acc.MinWidth <- value
        acc

    [<CustomOperation("minHeight")>]
    member this.MinHeight (acc: Window, value) =
        acc.MinHeight <- value
        acc

    [<CustomOperation("show")>]
    member this.Show (acc: Window) =
        acc.Show()
        acc

    [<CustomOperation("showDialog")>]
    member this.ShowDialog (acc: Window) =
        acc.ShowDialog().Wait()
        acc
    
    [<CustomOperation("run")>]
    member this.Run (acc: Window) =
        Application.Current.Run <| this.Show(acc)
        acc

    [<CustomOperation("title")>]
    member this.Title (acc: Window, title) =
        acc.Title <- title
        acc



type TextBlockBuilder() =
    member this.Yield(item: 'a): TextBlock = TextBlock()


    [<CustomOperation("text")>]
    member this.Text (acc: TextBlock, value) =
        acc.Text <- value
        acc

    [<CustomOperation("row")>]
    member this.Row (acc: TextBlock, value) =
        Grid.SetRow(acc, value)
        acc


type GridBuilder() =
    member this.Yield(item: 'a): Grid = Grid()

    
    [<CustomOperation("rowDefinitions")>]
    member this.RowDefintion (acc: Grid, heights: float[]) =
        acc.RowDefinitions.AddRange(heights |> Seq.map (fun f -> (f |> GridLength |> RowDefinition)))
        acc
    
    [<CustomOperation("rowDefinition")>]
    member this.RowDefinition (acc: Grid, height: float) =
        acc.RowDefinitions.Add(RowDefinition(GridLength(height)))
        acc

    [<CustomOperation("content")>]
    member this.Content (acc: Grid, value: IControl[]) =
        acc.Children.AddRange(value)
        acc

type ButtonBuilder() =
    member this.Yield(item: 'a): Button = Button()

    [<CustomOperation("text")>]
    member this.Text (acc: Button, content) =
        acc.Content <- content
        acc

    [<CustomOperation("row")>]
    member this.Row (acc: Button, value) =
        Grid.SetRow(acc, value)
        acc

    [<CustomOperation("click")>]
    member this.Click (acc: Button, callback) =
        acc.Click.Add(callback)
        acc


        

let window = WindowBuilder()
let button = ButtonBuilder()
let textBlock = TextBlockBuilder()
let grid = GridBuilder()