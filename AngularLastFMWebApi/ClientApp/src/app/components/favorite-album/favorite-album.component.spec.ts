import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FavoriteAlbumComponent } from './favorite-album.component';

describe('FavoriteAlbumComponent', () => {
  let component: FavoriteAlbumComponent;
  let fixture: ComponentFixture<FavoriteAlbumComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FavoriteAlbumComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FavoriteAlbumComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
