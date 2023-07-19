import {
    Component,
    Injector,
    OnInit,
    EventEmitter,
    Output
  } from '@angular/core';
  import { BsModalRef } from 'ngx-bootstrap/modal';
  import { forEach as _forEach, includes as _includes, map as _map } from 'lodash-es';
  import { AppComponentBase } from '@shared/app-component-base';
  import {
    StudentServiceProxy,
    UpdateStudentDto
  } from '@shared/service-proxies/service-proxies';
  
  @Component({
    templateUrl: './update-student.component.html'
  })
  export class UpdateStudentComponent extends AppComponentBase
    implements OnInit {
    saving = false;
    student = new UpdateStudentDto();
    id: number;
  
    @Output() onSave = new EventEmitter<any>();
  
    constructor(
      injector: Injector,
      public _studentService: StudentServiceProxy,
      public bsModalRef: BsModalRef
    ) {
      super(injector);
    }
  
    ngOnInit(): void {
      this._studentService.get(this.id).subscribe((result) => {
        this.student = result;
      });
    }
  
    save(): void {
      this.saving = true;
  
      this._studentService.updateStudent(this.student).subscribe(
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