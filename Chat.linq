from Chats in db.Chats
select new {
  ChatId = Chats.ChatId,
  UserId = Chats.UserId,
  ChatMessage = Chats.ChatMessage,
  DateTimeStamp = Chats.DateTimeStamp
}