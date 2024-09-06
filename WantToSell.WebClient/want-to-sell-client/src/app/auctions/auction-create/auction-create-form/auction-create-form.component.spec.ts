import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuctionCreateFormComponent } from './auction-create-form.component';

describe('AuctionCreateFormComponent', () => {
  let component: AuctionCreateFormComponent;
  let fixture: ComponentFixture<AuctionCreateFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AuctionCreateFormComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AuctionCreateFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
