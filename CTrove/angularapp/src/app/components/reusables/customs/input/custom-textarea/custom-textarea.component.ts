import { Component } from '@angular/core';
import { DefTextareaComponent } from '../../../def-textarea/def-textarea.component';
import { FIELD_INVALID, FIELD_REQUIRED } from 'src/Utilities/common/app-strings';

@Component({
  selector: 'app-custom-textarea',
  templateUrl: './custom-textarea.component.html',
  styleUrls: ['./custom-textarea.component.css']
})
export class CustomTextareaComponent extends DefTextareaComponent {

  getErrorMessage = (req: number = 0) => {

    if(req === 1)
      return FIELD_REQUIRED
    else 
      return FIELD_INVALID
  }
}
