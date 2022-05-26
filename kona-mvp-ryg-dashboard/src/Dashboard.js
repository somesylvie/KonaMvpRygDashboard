import React from 'react'
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import ProgressBar from 'react-bootstrap/ProgressBar'
import Card from 'react-bootstrap/Card'
import Form from 'react-bootstrap/Form'
import Button from 'react-bootstrap/Button'
import Alert from 'react-bootstrap/Alert'

class Dashboard extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            error: null,
            isLoaded: false,
            teamStats: [],
            manager: "",
            displayScope: "directteam"
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;

        this.setState({
            [name]: value
        });
        console.log(`Set ${name} to ${value}`)
    }

    handleSubmit(event) {
        const url=`https://localhost:7027/dashboard/${this.state.displayScope}/${this.state.manager}`

        // Clear out the error message in case the previous error was resolved
        this.setState({error: null})

        if (this.state.manager !== ""){
            fetch(url)
            .then(res => res.json())
            .then(
                (result) => {
                    console.log(result);
                this.setState({
                    isLoaded: true,
                    teamStats: result
                });
                },
                (error) => {
                this.setState({
                    isLoaded: true,
                    error
                });
                }
            )
        } else {
            this.setState({error: {message: "You must select a manager"}})
        }

        event.preventDefault();
    }

    render() {

    const { error, isLoaded, teamStats, displayScope, manager } = this.state;
        return (
        <Container>
            <Row className="p-3">
                <Form as={Card} className="w-100" >
                    <Card.Header><h2>What data do you want to see?</h2></Card.Header>
                    <Row className="p-3">
                        <Form.Select className="mx-2 px-2" name="manager" onChange={this.handleChange}>
                            <option value="">Choose a manager</option>
                            <option value="UQ3QMNZ4M">UQ3QMNZ4M</option>
                            <option value="UQ6157D2S">UQ6157D2S</option>
                            <option value="UQ5L65QKZ">UQ5L65QKZ</option>
                            <option value="U01URC62FJL">U01URC62FJL</option>
                            <option value="U02EZK58CMQ">U02EZK58CMQ</option>
                            <option value="U02SGN4NB9S">U02SGN4NB9S</option>
                            <option value="U02UB0BTJES">U02UB0BTJES</option>
                        </Form.Select>
                        <div key="displayScope" className="mx-2 px-2">
                            <Form.Check
                                label="View only my teams"
                                name="displayScope"
                                type="radio"
                                id="directteam"
                                onChange={this.handleChange}
                                checked={displayScope === "directteam"}
                                value="directteam"
                            />
                            <Form.Check
                                label="View all teams under me"
                                name="displayScope"
                                type="radio"
                                id="fullteam"
                                onChange={this.handleChange}
                                checked={displayScope === "fullteam"}
                                value="fullteam"
                            />
                        </div>
                    </Row>
                    
                    <Row className="p-3">
                        <Button variant="primary" className="w-100 mx-2 px-2" type="submit" onClick={this.handleSubmit}>
                            Show me the data!
                        </Button>
                    </Row>
                </Form>
            </Row>
        {
            error && 
            <Alert variant="danger">
                Error: { error.message }
            </Alert>
        }
        {
            !isLoaded && !error &&
            <div className="col">
            Loading...
            </div>
        } 
        {
            isLoaded && !error &&
            <Row xs={2} md={3}>
                    {
                        teamStats.map(teamStat => {
                            return (
                                <div className="p-3" key={teamStat.teamId + "div"}>
                                <Card key={teamStat.teamId}>
                                    <Card.Header>Team {teamStat.teamId}</Card.Header>
                                    <Card.Body>
                                        <Card.Title>Manager: {teamStat.managerId}</Card.Title>
                                        <ProgressBar key={teamStat.teamId + "stats"}>
                                            <ProgressBar variant="success" now={teamStat.greenCount} key={teamStat.teamId + "green"} />
                                            <ProgressBar variant="warning" now={teamStat.yellowCount} key={teamStat.teamId + "yellow"} />
                                            <ProgressBar variant="danger" now={teamStat.redCount} key={teamStat.teamId + "red"} />
                                        </ProgressBar>
                                    </Card.Body>
                                </Card>
                                </div>
                            )
                        })
                    }
            </Row>
        }
                
        </Container>
    )
    }
}

export default Dashboard;

