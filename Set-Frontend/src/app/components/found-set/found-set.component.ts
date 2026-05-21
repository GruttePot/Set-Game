import { Component, Input } from '@angular/core';
import { FoundSet } from '../../models/found-set';
import { DatePipe } from '@angular/common';
import { CardComponent } from '../card/card.component';

@Component({
  selector: 'found-set',
  templateUrl: 'found-set.component.html',
  styleUrls: ['found-set.component.scss'],
  standalone: true,
  imports: [CardComponent, DatePipe]
})
export class FoundSetComponent {
  @Input() foundSet!: FoundSet;
}
