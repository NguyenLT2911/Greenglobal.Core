import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { CoreComponent } from './components/core.component';
import { CoreService } from '@greenglobal/core';
import { of } from 'rxjs';

describe('CoreComponent', () => {
  let component: CoreComponent;
  let fixture: ComponentFixture<CoreComponent>;
  const mockCoreService = jasmine.createSpyObj('CoreService', {
    sample: of([]),
  });
  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [CoreComponent],
      providers: [
        {
          provide: CoreService,
          useValue: mockCoreService,
        },
      ],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CoreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
