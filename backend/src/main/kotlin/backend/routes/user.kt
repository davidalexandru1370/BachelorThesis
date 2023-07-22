package backend.routes

import io.ktor.http.*
import io.ktor.server.application.*
import io.ktor.server.response.*
import io.ktor.server.routing.*

public fun Route.userRoutes(){
    route("/user"){
        get(""){
            call.respondText ("User", contentType = ContentType.Text.Plain )
        }
    }
}