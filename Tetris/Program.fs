open Silk.NET.Input
open Silk.NET.Maths
open Silk.NET.Windowing
open Silk.NET.OpenGL

let load (window: IWindow) () =
    let input = window.CreateInput()
    let gl = window.CreateOpenGL()
    gl.ClearColor(0.2f, 0.7f, 0.2f, 1.f)
    printfn "Count: %A" input.Mice.Count

    match input.Mice |> List.ofSeq with
    | [ mouse ]
    | mouse :: _ -> mouse.add_Click (fun cursor button vector -> printfn "Clicked here: %A" vector)
    | [] -> ()

let render (window: IWindow) (n: float) =
    window
        .CreateOpenGL()
        .Clear(ClearBufferMask.ColorBufferBit)

[<EntryPoint>]
let main _ =
    let mutable windowOptions = WindowOptions.Default
    windowOptions.Size <- new Vector2D<int>(800, 600)
    let window = Window.Create(windowOptions)

    window.add_Load (load window)
    window.add_Update (fun (n: float) -> ())
    window.add_Render (render window)
    window.add_Resize (fun (v: Vector2D<int>) -> ())

    window.Run()
    0
