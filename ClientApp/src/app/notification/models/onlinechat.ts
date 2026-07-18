export interface OnlineChat {
      OnlineChatId: number,
      sendBaseSchoolNameId: number,
      receivedBaseSchoolNameId: number,
      senderRole: string,
      receiverRole: string,
      notes: string,
      reciverSeenStatus:number,
      endDate:Date,
      isActive: boolean
}
