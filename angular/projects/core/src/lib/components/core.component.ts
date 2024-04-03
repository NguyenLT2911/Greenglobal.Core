import { Component, OnInit } from '@angular/core';
import { CoreService } from '../services/core.service';

@Component({
  selector: 'lib-core',
  template: ` <p>core works!</p> `,
  styles: [],
})
export class CoreComponent implements OnInit {
  constructor(private service: CoreService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
