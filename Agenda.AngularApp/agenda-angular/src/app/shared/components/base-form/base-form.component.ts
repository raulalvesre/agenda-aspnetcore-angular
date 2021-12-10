import { Directive } from '@angular/core';
import { FormArray, FormControl, FormGroup } from '@angular/forms';

@Directive()
export abstract class BaseFormComponent {

  formulario: FormGroup;

  constructor() { }

  abstract submit(): any;

  onSubmit() {
    if (this.formulario.valid) {
      this.submit();
    } else {
      this.verificaValidacoesForm(this.formulario);
    }
  }

  verificaValidacoesForm(formGroup: FormGroup | FormArray) {
    Object.keys(formGroup.controls).forEach(campo => {
      const controle = formGroup.get(campo);
      controle.markAsDirty();
      controle.markAsTouched();
      if (controle instanceof FormGroup || controle instanceof FormArray) {
        this.verificaValidacoesForm(controle);
      }
    });
  }

  getFormControl(formControlName: string): FormControl {
    return this.formulario?.get(formControlName) as FormControl;
  }

}
