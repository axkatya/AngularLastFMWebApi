import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AlbumComponent } from './components/album/album.component';
import { ArtistComponent } from './components/artist/artist.component';

const routes: Routes = [
  { path: '', redirectTo: '/albums', pathMatch: 'full' },
  { path: 'albums', component: AlbumComponent },
  { path: 'artists', component: ArtistComponent },
  { path: 'artists/:artistName', component: ArtistComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
