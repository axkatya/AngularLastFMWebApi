import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';

import { AuthGuard } from './guards/auth.guard';

import { AlertComponent } from './components/alert/alert.component';
import { AppComponent } from './components/app.component';

import { AlbumComponent } from './components/album/album.component';
import { AlbumListComponent } from './components/album-list/album-list.component';
import { AlbumItemComponent } from './components/album-item/album-item.component';
import { FavoriteAlbumComponent } from './components/favorite-album/favorite-album.component';

import { ArtistComponent } from './components/artist/artist.component';
import { ArtistItemComponent } from './components/artist-item/artist-item.component';

import { AlbumService } from './services/album.service';
import { FavoriteAlbumService } from './services/favorite-album.service';
import { ArtistService } from './services/artist.service';
import { FavoriteArtistService } from './services/favorite-artist.service';
import { AuthenticationService } from './services/authentication.service';
import { AlertService } from './services/alert.service';
import { UserService } from './services/user.service';

import { AppRoutingModule } from './/app-routing.module';
import { RouterLinkDirectiveStub } from './testing/router-link-directive-stub';
import { TrackListComponent } from './components/track-list/track-list.component';
import { TrackItemComponent } from './components/track-item/track-item.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { FavoriteButtonComponent } from './components/favorite-button/favorite-button.component';
import { FavoriteArtistComponent } from './components/favorite-artist/favorite-artist.component';
import { ArtistListComponent } from './components/artist-list/artist-list.component';



@NgModule({
  declarations: [
    FavoriteButtonComponent,
    AlertComponent,
    AppComponent,
    LoginComponent,
    RegisterComponent,
    AlbumComponent,
    ArtistComponent,
    ArtistItemComponent,
    AlbumListComponent,
    AlbumItemComponent,
    RouterLinkDirectiveStub,
    TrackListComponent,
    TrackItemComponent,
    FavoriteAlbumComponent,
    FavoriteArtistComponent,
    ArtistListComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    AppRoutingModule,
    FormsModule 
  ],
  providers: [
    AuthGuard,
    AlbumService,
    ArtistService,
    AuthenticationService,
    AlertService,
    UserService,
    FavoriteAlbumService,
    FavoriteArtistService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
