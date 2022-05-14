module Domain

open System

module FinalData =

    type RivenStat =
        { Name: string
          Value: string
          Positive: bool }

    type Riven =
        { ImageUrl: Uri
          Price: int option
          RivenStats: RivenStat list }

    type MessageInfo =
        { MessageId: Guid
          User: string
          MessageText: string }

    type RivenMessage =
        { MessageInfo: MessageInfo
          Rivens: Riven list }

    type OtherMessage =
        { MessageInfo: MessageInfo }

    type ChatMessages = RivenMessage | OtherMessage
