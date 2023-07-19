import { Component, Injector } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
  PagedListingComponentBase,
  PagedResultDto,
  PagedRequestDto
} from 'shared/paged-listing-component-base';
import {
  StudentServiceProxy,
  StudentDto
  
} from '@shared/service-proxies/service-proxies';
import { CreateStudentComponent } from './create-student/create-student.component';
import { UpdateStudentComponent } from './update-student/update-student.component';

class PagedStudentRequestDto extends PagedRequestDto {
  keyword: string;
  isActive: boolean | null;
}

@Component({
  templateUrl: './students.component.html',
  animations: [appModuleAnimation()]
})
export class StudentsComponent extends PagedListingComponentBase<StudentDto> {
  students: StudentDto[] = [];
  keyword = '';
  isActive: boolean | null;
  advancedFiltersVisible = false;

  constructor(
    injector: Injector,
    private _studentService: StudentServiceProxy,
    private _modalService: BsModalService
  ) {
    super(injector);
  }

  createStudent(): void {
    this.showCreateOrUpdateStudentDialog();
  }

  updateStudent(student: StudentDto): void {
    this.showCreateOrUpdateStudentDialog(student.id);
  }

  clearFilters(): void {
    this.keyword = '';
    this.isActive = undefined;
    this.getDataPage(1);
  }

  protected list(
    request: PagedStudentRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;
    

    this._studentService
      .getAll(
        request.keyword,
        request.skipCount,
        request.maxResultCount
      )
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: PagedResultDto) => {
        this.students = result.items;
        this.showPaging(result, pageNumber);
      });
  }

  protected delete(student: StudentDto): void {
    abp.message.confirm(
      this.l('UserDeleteWarningMessage', student.lastName),
      undefined,
      (result: boolean) => {
        if (result) {
          this._studentService.deleteStudent(student.id).subscribe(() => {
            abp.notify.success(this.l('SuccessfullyDeleted'));
            this.refresh();
          });
        }
      }
    );
  }

  private showCreateOrUpdateStudentDialog(id?: number): void {
    let createOrUpdateStudentDialog: BsModalRef;
    if (!id) {
      createOrUpdateStudentDialog = this._modalService.show(
        CreateStudentComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
        createOrUpdateStudentDialog = this._modalService.show(
        UpdateStudentComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }

    createOrUpdateStudentDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }
}