import { Component } from '@angular/core';
import { FIELD_EXISTING_EMAIL, FIELD_INVALID, FIELD_INVALID_EMAIL, FIELD_REQUIRED } from 'src/Utilities/common/app-strings';
import { DefInputComponent } from '../../../def-input/def-input.component';

@Component({
  selector: 'app-custom-input-field',
  templateUrl: './custom-input-field.component.html',
  styleUrls: ['./custom-input-field.component.css']
})
export class CustomInputFieldComponent extends DefInputComponent {

  getErrorMessage = (req: number = 0) => {

    if(req === 1)
      return FIELD_REQUIRED
    else if(req === 2)
      return FIELD_INVALID_EMAIL
    else if(req === 3)
      return FIELD_EXISTING_EMAIL
    else 
      return FIELD_INVALID
  }
}
