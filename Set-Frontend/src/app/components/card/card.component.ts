import {Component, Input, OnInit} from '@angular/core';
import {Card, CardColour, CardFilling, CardNumber, CardShape} from '../../models/card';

@Component({
  selector: 'card',
  templateUrl: 'card.component.html',
  styleUrls: ['card.component.scss']
})
export class CardComponent implements OnInit  {
  @Input() card!: Card;
  @Input() selected: boolean = false;

  shapeHref: string = '';
  strokeValue: string = '';
  fillValue: string = '';
  shapeArray: number[] = [];

  ngOnInit() {
    // Test card voor het inladen
    if (!this.card) {
      this.card = {
        id: 1,
        colour: CardColour.Purple,
        shape: CardShape.Oval,
        filling: CardFilling.Striped,
        number: CardNumber.Three,
      };
    }

    this.shapeHref = `#card_${this.card.shape}`
    this.strokeValue = this.card.colour;
    this.fillValue = this.card.filling;
    this.shapeArray = Array.from({ length: this.getShapeCount() }, (_, i) => i);
  }

  getShapeCount(): number {
    const numbers: { [key in CardNumber]: number } = {
      [CardNumber.One]: 1,
      [CardNumber.Two]: 2,
      [CardNumber.Three]: 3,
    };
    return numbers[this.card.number];
  }

}
