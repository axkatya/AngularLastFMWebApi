import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TrackItemComponent } from './track-item.component';
import { NO_ERRORS_SCHEMA } from '@angular/core';

describe('TrackItemComponent', () => {
  let component: TrackItemComponent;
  let fixture: ComponentFixture<TrackItemComponent>;

  beforeEach(async(() => {
	TestBed.configureTestingModule({
	  declarations: [TrackItemComponent],
	  schemas: [NO_ERRORS_SCHEMA]
	})
	.compileComponents();
  }));

  beforeEach(() => {
	fixture = TestBed.createComponent(TrackItemComponent);
	component = fixture.componentInstance;
	fixture.detectChanges();
  });

  it('should create', () => {
	expect(component).toBeTruthy();
  });
});
