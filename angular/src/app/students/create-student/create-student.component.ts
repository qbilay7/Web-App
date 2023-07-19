import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output
} from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { forEach as _forEach, map as _map } from 'lodash-es';
import { AppComponentBase } from '@shared/app-component-base';

import { StudentServiceProxy, CreateStudentDto} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: './create-student.component.html'
})
export class CreateStudentComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  student = new CreateStudentDto();
  
  

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _studentService: StudentServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    
  }


  save(): void {
    this.saving = true;

    this._studentService.createStudent(this.student).subscribe(
      () => {
        this.bsModalRef.hide();
        this.onSave.emit();
      },
      () => {
        this.saving = false;
      }
    );
  }
}

