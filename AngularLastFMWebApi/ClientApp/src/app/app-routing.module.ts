import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';

import { AlbumComponent } from './components/album/album.component';
import { FavoriteAlbumComponent } from './components/favorite-album/favorite-album.component';
import { ArtistComponent } from './components/artist/artist.component';
import { FavoriteArtistComponent } from './components/favorite-artist/favorite-artist.component';
import { LoginComponent } from './/components/login/login.component';
import { RegisterComponent } from './/components/register/register.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'albums', component: AlbumComponent, canActivate: [AuthGuard] },
  { path: 'favoriteAlbums', component: FavoriteAlbumComponent, canActivate: [AuthGuard] },
  { path: 'artists/:artistName', component: ArtistComponent, canActivate: [AuthGuard] },
  { path: 'artists', component: ArtistComponent, canActivate: [AuthGuard] },
  { path: 'favoriteArtists', component: FavoriteArtistComponent, canActivate: [AuthGuard] },
  { path: '', component: AlbumComponent, canActivate: [AuthGuard] },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
