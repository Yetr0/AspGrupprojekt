ASP.NET har en startup.cs klass som bland annat skapar routs till alla sidor som ligger i pages mappen, den sätter ihop allt som behövs för att sidan ska funger. Wwwwroot innehåller statiska filer så som css filer, javascript och bilder. Razor pages språket tillåter att c# kod används för att rendera en sida

Razor pages består av två olika delar. Content page och page model. Page model är sidans model och används för att skicka information till html delen av en razor page. För att t.ex. kunna hämta information när man ber om sidan, eller för att göra något när man submitar ett formulär som sedan kan användas. Content page är html delen av sidan, det är detta som servern skickar tillbaka till clienten.

MVC står för model view controller. View är delen som returneras, bestämmer hur sidan ska se ut. Controller håller reda på logik, vad ska hända när man gör en port request eller liknande. Model innehåller det som ska finnas på sidan, t.ex. info från databasen.
