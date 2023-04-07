import { useState, useEffect } from "react";
import ListGroup from "react-bootstrap/ListGroup";
import { Link } from "react-router-dom";

const ArchivesWidget = () => {
    const [amountList, setAmountList] = useState([]);

    useEffect(() => {
        setAmountList(["December", "November", "October", "September", "August", "July", "June", "May", "April", "March", "February", "January"]);
    }, [])

    return (
        <div className="mb-4">
            <h3 className="text-success mb-2">
                Archives
            </h3>
            {
                amountList.length > 0 &&
                <ListGroup>
                    {
                        amountList.map((item, index) => {
                            return (
                                <ListGroup.Item key={index}>
                                    <Link
                                        title={item}
                                        key={index}
                                        className="text-decoration-none">
                                        {item}
                                        <span>&nbsp;(1)</span>
                                    </Link>
                                </ListGroup.Item>
                            );
                        })
                    }
                </ListGroup>
            }
        </div>
    );
}

export default ArchivesWidget;