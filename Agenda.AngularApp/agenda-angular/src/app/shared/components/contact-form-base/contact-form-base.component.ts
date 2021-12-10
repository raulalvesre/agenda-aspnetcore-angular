import { Directive, Inject, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatTable } from '@angular/material/table';
import { of, Subject } from 'rxjs';
import { map, startWith, takeUntil } from 'rxjs/operators';
import { ContactTelephoneApiResponse } from '../../interfaces/contact/contact-telephone-api-response';
import { ContactTelephoneApiRequest } from '../../interfaces/contact/contact-telephone-request';
import { BaseContactService } from '../../services/base-contact.service';
import { TelephoneType } from '../../telephone-types';
import { BaseFormComponent } from '../base-form/base-form.component';
import { ContactApiResponse } from './../../interfaces/contact/contact-api-response';

@Directive()
export abstract class ContactFormBaseComponent<T extends ContactApiResponse>
  extends BaseFormComponent
  implements OnInit, OnDestroy {

  protected endSubscriptionsNotifier = new Subject();
  contact: T;
  telephoneTypes: any[];
  displayedColumns: string[] = ["telephoneNumber", "description", "type", "actions"];
  @ViewChild(MatTable) telephoneTable: MatTable<any>;

  get telephones() {
    return this.formulario.get('telephones') as FormArray;
  }

  constructor(
    protected formBuilder: FormBuilder,
    protected service: BaseContactService,
    public dialogRef: MatDialogRef<any>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    super();
    this.telephoneTypes = data.telephoneTypes;
  }

  ngOnInit() {
    this.contact = this.data.contact;

    this.formulario = this.formBuilder.group({
      name: [this.contact?.name, [Validators.required, Validators.maxLength(200)]],
      telephones: this.formBuilder.array([])
    });

    if (this.contact != null)
      this.populateTelephoneArray();
  }

  protected populateTelephoneArray() {
    this.contact.telephones.forEach((tel: ContactTelephoneApiResponse) => {
      let telephone: ContactTelephoneApiRequest = {
        type: this.getExistingTelephoneTypeIdFromTelephoneTypeName(tel.type),
        telephoneNumber: tel.telephoneFormatted,
        description: tel.description
      };

      let telFormGroup = this.createTelephoneFormGroup(telephone);
      this.telephones.push(telFormGroup);
    });
  }

  protected createTelephoneFormGroup(tel: ContactTelephoneApiRequest = null): FormGroup {
    let telType = tel?.type ?? 2;

    let telGroup = this.formBuilder.group({
      type: telType,
      description: [tel?.description, [Validators.required, Validators.maxLength(50)]],
      telephoneNumber: [tel?.telephoneNumber, null, this.isTelephoneNumberAlreadyRegistered.bind(this)]
    });

    telGroup.get('type').valueChanges
      .pipe(
        startWith(telType),
        takeUntil(this.endSubscriptionsNotifier)
      )
      .subscribe(telephoneType => {
        let telephoneNumberControl = telGroup.get('telephoneNumber');
        let telephoneCommonValidators = [Validators.required, this.isTelephoneAlreadyInFormArray.bind(this)];

        if (telephoneType == TelephoneType.Landline)
          telephoneNumberControl.setValidators([...telephoneCommonValidators, this.isLandlineNumberValid]);

        if (telephoneType == TelephoneType.Commercial)
          telephoneNumberControl.setValidators([...telephoneCommonValidators, this.isCommercialNumberValid]);

        if (telephoneType == TelephoneType.Cellphone)
          telephoneNumberControl.setValidators([...telephoneCommonValidators, this.isCellphoneNumberValid]);

        telephoneNumberControl.updateValueAndValidity();
      });

    return telGroup;
  }

  protected isLandlineNumberValid(formControl: AbstractControl): { [key: string]: boolean } | null {
    let value = formControl?.value;
    if (value == null)
      return null;

    let landlinePattern = /^\((?:[14689][1-9]|2[12478]|3[1234578]|5[1345]|7[134579])\) [2-8][0-9]{3}\-[0-9]{4}$/;
    if (landlinePattern.test(value))
      return null;

    return { invalid: true };
  }

  protected isCellphoneNumberValid(formControl: AbstractControl): { [key: string]: boolean } | null {
    let value = formControl?.value;
    if (value == null)
      return null;

    let cellphonePattern = /^\((?:[14689][1-9]|2[12478]|3[1234578]|5[1345]|7[134579])\) 9[1-9][0-9]{3}\-[0-9]{4}$/;
    if (cellphonePattern.test(value))
      return null;

    return { invalid: true };
  }

  protected isCommercialNumberValid(formControl: AbstractControl): { [key: string]: boolean } | null {
    let value = formControl?.value;
    if (value == null)
      return null;

    let commercialPattern = /^\((?:[14689][1-9]|2[12478]|3[1234578]|5[1345]|7[134579])\) (?:[2-8]|9[1-9])[0-9]{3}\-[0-9]{4}$/;
    if (commercialPattern.test(value))
      return null;

    return { invalid: true };
  }

  protected getExistingTelephoneTypeIdFromTelephoneTypeName(telephoneTypeName: string): number {
    return this.telephoneTypes.find(t => t.name.localeCompare(telephoneTypeName) == 0).id;
  }

  addTelephone() {
    this.telephones.push(this.createTelephoneFormGroup());
    this.telephoneTable.renderRows();
  }

  deleteTelephone(index: number) {
    this.telephones.removeAt(index);
    this.telephoneTable.renderRows();
  }

  protected isTelephoneAlreadyInFormArray(formControl: AbstractControl): { [key: string]: boolean } | null {
    let value = formControl?.value
    //null checks pra não der erro quando iniciar
    if (!formControl.root.value || !formControl.parent || !value || !formControl.root.value.telephones) {
      return null;
    }

    let currentTelephones = this.telephones.controls
      .map((x: any) => x.controls['telephoneNumber'].value);
    
    let occurrences: number = currentTelephones
      .reduce((ac: any, tF: any) => {
        if (tF === value) {
          ac++;
        }

        return ac;
      }, 0);

    if (occurrences > 1)
      return { duplicate: true };

    return null;
  }

  isTelephoneNumberAlreadyRegistered(formControl: AbstractControl) {
    let value = formControl?.value;
    if (this.contact?.telephones.some((t: any) => t.telephoneFormatted.localeCompare(value) == 0))
      return of(null);

    return this.service.isTelephoneNumberAlreadyRegistered(value)
      .pipe(map(isAlreadyRegistered => isAlreadyRegistered ? { alreadyRegistered: true } : null));
  }

  getTelephoneFormGroup(index: number) {
    return this.telephones.controls[index] as FormGroup;
  }

  getTelephoneFormControl(index: number, controlName: string) {
    return this.telephones.controls[index].get(controlName) as FormControl;
  }

  submit() {
    if (this.contact == null) {
      this.addContact();
    } else {
      this.updateContact();
    }
  }

  protected addContact() {
    let contactApiRequest = this.createContactApiRequest();

    this.service.create(contactApiRequest)
      .subscribe(r => {
        this.dialogRef.close({
          operation: 'add'
        });
      });
  }

  protected updateContact() {
    let contactId = this.contact.id;
    let contactApiRequest = this.createContactApiRequest();

    this.service.update(contactId, contactApiRequest)
      .subscribe(r => {
        this.dialogRef.close({
          operation: 'update'
        });
      });
  }

  abstract createContactApiRequest(): any;

  ngOnDestroy(): void {
    this.endSubscriptionsNotifier.next();
    this.endSubscriptionsNotifier.complete();
  }

}
