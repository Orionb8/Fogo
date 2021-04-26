import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ServiceService {

  constructor() { }
  
  hideMenu: BehaviorSubject<boolean> = new BehaviorSubject(false);

  hide() {
    this.hideMenu.next(false);
  }
  
  show() {
    this.hideMenu.next(true);
  }
}
