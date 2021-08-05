namespace App

open Feliz
open Feliz.Router
open Feliz.Bulma

type Components =
    /// <summary>
    /// The simplest possible React component.
    /// Shows a header with the text Hello World
    /// </summary>
    [<ReactComponent>]
    static member HelloWorld() =
        let sliderChanged (value: string) =
            let intValue = int value
            match intValue with
            | x when x > 50 -> color.isDanger
            | x when x < 50 -> color.isSuccess
            | _ -> color.isInfo
    
        Bulma.container [
            Bulma.column [
                Bulma.title "SwimBuddy"
                Html.div "Helping you prepare your swim training"
                Bulma.card [
                    Bulma.cardImage [
                        Bulma.image [
                            image.is4by3
                            prop.children [
                                Html.img [ 
                                    prop.alt "Swimmer stretching"
                                    prop.src "https://www.orthocarolina.com/imagecache/mobile/compReg/swimmer_stretch_1.jpeg"
                                ]
                            ]
                        ]
                    ]
                    Bulma.cardHeader [
                        Bulma.cardHeaderTitle.p "SwimBuddy"
                    ]
                    Bulma.cardContent [
                        prop.text "Select the difficulty"
                    ]
                ] 
            ]
        ]

    /// <summary>
    /// A stateful React component that maintains a counter
    /// </summary>
    [<ReactComponent>]
    static member Counter() =
        let (count, setCount) = React.useState(0)
        Html.div [
            Html.h1 count
            Html.button [
                prop.onClick (fun _ -> setCount(count + 1))
                prop.text "Increment"
            ]
        ]

    /// <summary>
    /// A React component that uses Feliz.Router
    /// to determine what to show based on the current URL
    /// </summary>
    [<ReactComponent>]
    static member Router() =
        let (currentUrl, updateUrl) = React.useState(Router.currentUrl())
        React.router [
            router.onUrlChanged updateUrl
            router.children [
                match currentUrl with
                | [ ] -> Html.h1 "Index"
                | [ "hello" ] -> Components.HelloWorld()
                | [ "counter" ] -> Components.Counter()
                | otherwise -> Html.h1 "Not found"
            ]
        ]