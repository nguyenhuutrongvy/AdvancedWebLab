import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEnvelopeCircleCheck } from "@fortawesome/free-solid-svg-icons";

const SubscribeForm = () => {

    return (
        <div className="mb-4">
            <h3 className="text-success mb-2">
                Đăng ký nhận tin mới
            </h3>
            <Form>
                <Form.Group className="input-group mb-3">
                    <Form.Control
                        type="text"
                        aria-label="Enter your email"
                        aria-describedby="btnSubscribe"
                        placeholder="Enter your email" />
                    <Button
                        id='btnSubscribe'
                        variant='outline-secondary'
                        type='submit'>
                        <FontAwesomeIcon icon={faEnvelopeCircleCheck} />
                    </Button>
                </Form.Group>
            </Form>
        </div>
    );
};

export default SubscribeForm;