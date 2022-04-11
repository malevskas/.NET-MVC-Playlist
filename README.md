# .NET-MVC-Playlist
A simple WEB app for creating and editing albums and playlists.

The models used in this project are: User, Album, Playlist, Song and SongPlaylist which represents the M:M relation between Song and Playlist.

Displayed for each of the albums is its cover, title and duration. 
By clicking the title, a new view is displayed in which are listed all the songs contained in that album.
By clicking the artist, a new view is displayed in which are listed all of their albums, whose titles can again be clicked.
The albums can be filtered by their title and artist.

Displayed for each of the songs is its album cover, title, album title, artist and duration.
By clicking the album title, a new view is displayed in which are listed all the songs it contains.
By clicking the artist, a new view is displayed in which are listed all of their albums, whose titles can again be clicked.
The songs can be filtered by their title, album and artist.
When adding a new song to the database, an album has to be selected and its duration is updated.

Displayed for each of the playlists is its cover photo, name, duration and the user it belongs to.
By clicking the name, a new view is displayed in which are listed all the songs the playlist contains, as well as the album they are from, the artist and its duration.
There is an option for removing the song from the playlist.
When adding a new song to the playlist, its duration is updated.

Displayed for each of the users is their profile picture, full name, username and type (user or artist).
