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
export function Login() {
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

  var { status: loginStatus, message } = useStream(
    ctrl.stream$.pipe(map((state) => state.asyncStatus)),
    ctrl.state.asyncStatus
  );
  if (loginStatus == AsyncStatus.Success) {
    setTimeout(() => {
      history.push('/todo');
    }, 0);
  }
  function submit() {
    if (validate()) {
      ctrl.login(formData());
    }
  }
  return (
    <Form>
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

      <Button
        disabled={loginStatus == AsyncStatus.Pending}
        onClick={submit}
        type="button"
      >
        Login {loginStatus == AsyncStatus.Pending && <Spinner>.</Spinner>}
      </Button>
      {loginStatus === AsyncStatus.Error && (
        <Alert color="danger">{message}</Alert>
      )}
    </Form>
  );
}
