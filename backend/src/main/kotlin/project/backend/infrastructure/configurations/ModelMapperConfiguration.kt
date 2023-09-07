package project.backend.infrastructure.configurations

import org.modelmapper.ModelMapper
import org.springframework.context.annotation.Bean
import org.springframework.context.annotation.Configuration

@Configuration
class ModelMapperConfiguration {
    @Bean
    fun modelMapper(): ModelMapper {
        val modelMapper: ModelMapper = ModelMapper();
        modelMapperConfiguration(modelMapper)
        return modelMapper;
    }

    private fun modelMapperConfiguration(modelMapper: ModelMapper) {
        modelMapper.configuration.isFieldMatchingEnabled = true
        modelMapper.configuration.isSkipNullEnabled = true
    }
}