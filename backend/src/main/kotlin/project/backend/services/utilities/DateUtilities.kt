<<<<<<<< HEAD:backend/src/main/kotlin/project/backend/services/utilities/DateUtilities.kt
package project.backend.services.utilities
========
package project.backend.businessLogic.utilities
>>>>>>>> da94f09cefb24785e68474a3021cf8c4c73ab5f6:backend/src/main/kotlin/project/backend/businessLogic/utilities/DateUtilities.kt

import java.util.*

object DateUtilities {
    fun addMinutes(date: Date, minutes: Int): Date {
        var dateInUnixTime: Long = date.time
        return Date(dateInUnixTime + minutes * 60 * 1000)
    }
}
