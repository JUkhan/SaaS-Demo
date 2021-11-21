import { useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import {
  Form,
  FormGroup,
  Input,
  Label,
  Button,
  FormFeedback,
  Spinner,
  Alert,
} from 'reactstrap';
import { useForm } from '@jukhan/react-form';
import { Get } from 'ajwahjs';
import { AppStateController, AsyncStatus } from '../state/AppStateController';
import { useStream } from '../hooks';
import { map } from 'rxjs';
export function Register() {
  const ctrl = Get(AppStateController);
  let history = useHistory();
  const {
    validate,
    setValue,
    getValue,
    formData,
    setValidation,
    getStatus,
    getMessage,
  } = useForm();

  useEffect(() => {
    setValidation('userName', (val) =>
      val ? ['', ''] : ['warning', 'user name is required']
    );
    setValidation('password', (val) =>
      val ? ['', ''] : ['warning', 'password is required']
    );
  }, []);

  var { status: registerStatus } = useStream(
    ctrl.stream$.pipe(map((state) => state.asyncStatus)),
    ctrl.state.asyncStatus
  );
  if (registerStatus == AsyncStatus.Success) {
    setTimeout(() => {
      history.push('/todo');
    }, 0);
  }
  function submit() {
    if (validate()) {
      ctrl.register(formData());
    }
  }
  return (
    <Form>
      <FormGroup>
        <Label for="firstName">First Name</Label>
        <Input
          value={getValue('firstName')}
          onChange={(e) => setValue('firstName', e.target.value)}
          id="firstName"
          name="firstName"
          placeholder="first name"
          type="text"
        />
      </FormGroup>
      <FormGroup>
        <Label for="lastName">Last Name</Label>
        <Input
          value={getValue('lastName')}
          onChange={(e) => setValue('lastName', e.target.value)}
          id="lastName"
          name="lastName"
          placeholder="last name"
          type="text"
        />
      </FormGroup>
      <FormGroup>
        <Label for="userName">User Name</Label>
        <Input
          invalid={getStatus('userName')}
          id="userName"
          name="userName"
          value={getValue('userName')}
          onChange={(e) => setValue('userName', e.target.value)}
          placeholder="user name"
          type="text"
        />
        <FormFeedback>{getMessage('userName')}</FormFeedback>
      </FormGroup>
      <FormGroup>
        <Label for="email">Email</Label>
        <Input
          value={getValue('email')}
          onChange={(e) => setValue('email', e.target.value)}
          id="email"
          name="email"
          placeholder="email"
          type="email"
        />
      </FormGroup>
      <FormGroup>
        <Label for="password">Password</Label>
        <Input
          value={getValue('password')}
          onChange={(e) => setValue('password', e.target.value)}
          id="password"
          invalid={getStatus('password')}
          name="password"
          placeholder="password"
          type="text"
        />
        <FormFeedback>{getMessage('password')}</FormFeedback>
      </FormGroup>
      <FormGroup check>
        <Label check>
          {' '}
          <Input
            value={getValue('isPayingUser')}
            onChange={(e) => setValue('isPayingUser', e.target.checked)}
            type="checkbox"
          />{' '}
          Is Paying User
        </Label>
      </FormGroup>
      {(registerStatus == AsyncStatus.Idle ||
        registerStatus == AsyncStatus.Success) && (
        <Button onClick={submit} type="button">
          Register
        </Button>
      )}
      {registerStatus == AsyncStatus.Pending && (
        <Alert color="primary">
          <Spinner>.</Spinner> Creating Database and Pushing Dummy Data.
        </Alert>
      )}
    </Form>
  );
}
