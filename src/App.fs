module App

open Browser.Dom
open Fetch
open Thoth.Fetch
open Elmish
open Fable.React.Standard
open Fable.React.Helpers
open Fable.React.Props

type Model =
    { Count: int
      Message: string option
      Fetching: bool }

type Msg =
    | Increment
    | FetchingMessage
    | MessageReceived of string
    | MessageError of exn

let init () =
    { Count = 0
      Message = None
      Fetching = false },
    Cmd.none

let update msg model =
    match msg with
    | Increment -> { model with Count = model.Count + 1 }, Cmd.none
    | FetchingMessage ->
        let getMessage () =
            promise {
                let! message =
                    Fetch.get<unit, string> (
                        "/api/GetMessage?name=FSharp",
                        headers = [ HttpRequestHeaders.Accept "application/json" ]
                    )

                return message
            }

        { model with Fetching = true }, Cmd.OfPromise.either getMessage () MessageReceived MessageError
    | MessageReceived message ->
        { model with
              Message = Some message
              Fetching = false },
        Cmd.none
    | MessageError err ->
        printfn "%O" err

        { model with
              Fetching = false
              Message = None },
        Cmd.none

let root model dispatch =
    div [] [
        p [] [ str "Elmish is running" ]
        p [] [ str "You can click this button" ]
        if model.Count = 0 then
            button [ OnClick(fun _ -> dispatch Increment) ] [
                str "Click me"
            ]
        else
            button [ OnClick(fun _ -> dispatch Increment) ] [
                sprintf "You clicked: %d time(s)" model.Count
                |> str
            ]

        br []
        button [ OnClick(fun _ -> dispatch FetchingMessage) ] [
            str "Get a message from the server"
        ]
        match (model.Fetching, model.Message) with
        | (false, None) -> ()
        | (false, Some msg) -> p [] [ str msg ]
        | (true, _) -> p [] [ str "Fetching from server..." ]
    ]

open Elmish.React
open Elmish.HMR

// App
Program.mkProgram init update root
|> Program.withReactBatched "elmish-app"
|> Program.run
