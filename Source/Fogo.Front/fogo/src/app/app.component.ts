import { Component, Inject } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { filter } from 'rxjs/operators';
import { ServiceService } from './service.service';

@Component({
  selector: 'fogo-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'fogo';
  
  hide = false;

  constructor(public service: ServiceService) {
    service.show();
  }

  ngOnInit(): void {
    this.service.hideMenu.subscribe(x => {
      this.hide = x;
    });
  }

  // constructor(private router: Router){
    
  //   router.events.pipe(filter(event => event instanceof NavigationEnd))
  //         .subscribe(event => 
  //          {
  //             console.log(event);
  //          });
  //   }
}
