import { Component, EventEmitter, Input, Output, OnChanges, SimpleChanges, SimpleChange} from '@angular/core';
import { Album } from '../../models/album';


@Component({
  selector: 'app-favorite-button',
  templateUrl: './favorite-button.component.html'
})
export class FavoriteButtonComponent implements OnChanges {
  constructor(
  ) { }

  @Input() isFavorite: boolean;
  private _isFavorite: boolean;
  @Output() toggle = new EventEmitter();

  ngOnChanges(changes: SimpleChanges) {
	const isFavoriteChange: SimpleChange = changes.isFavorite;
	this._isFavorite = isFavoriteChange.currentValue;
  }

  toggleFavorite() {
	  return this.toggle.emit();
  }
}
